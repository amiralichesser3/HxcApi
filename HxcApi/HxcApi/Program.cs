using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Dapper;
using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.DataAccess.Contracts.Todos.Queries;
using HxcApi.DataAccess.DapperImplementation.Todos.Ioc;
using HxcApi.DataAccess.DapperImplementation.TypeHandlers;
using HxcApi.Events.Todos.Ioc;
using HxcApi.ExceptionHandling.Middleware;
using HxcApi.ExceptionHandling.Serilog;
using HxcApi.ExceptionHandling.Todo;
using HxcApi.Utility;
using HxcCommon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Serilog;

[module:DapperAot]

Environment.SetEnvironmentVariable("CORECLR_GLOBAL_INVARIANT", "1");
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
var builder = WebApplication.CreateSlimBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Development.Sensitive.json", optional: true, reloadOnChange: true); 
builder.Configuration.AddJsonFile("appsettings.Production.Sensitive.json", optional: true, reloadOnChange: true); 
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

string authority = builder.Configuration["Auth0:Domain"]!;
string audience = builder.Configuration["Auth0:Audience"]!;
string readConString = builder.Configuration["ConnectionStrings:HxcDb_Read"]!;
string writeConString = builder.Configuration["ConnectionStrings:HxcDb_Write"]!;
string appVersion = Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
    .InformationalVersion;

LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Level >= Serilog.Events.LogEventLevel.Warning)
        .WriteTo.Sink(new HxcSerilogSink(writeConString, appVersion[..appVersion.IndexOf('+')])));


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = authority;
        c.Audience = audience;

        c.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("organization", policy =>
    {
        policy.RequireRole("organization");
    });
    options.AddPolicy("admin", policy =>
    {
        policy.RequireRole("admin");
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddKeyedScoped<SqlConnection>("ReadSqlConnection", ((provider, o) => new SqlConnection(readConString)));
builder.Services.AddKeyedScoped<SqlConnection>("WriteSqlConnection", ((provider, o) => new SqlConnection(writeConString)));

builder.Services.RegisterTodoServices();
builder.Services.RegisterTodoEvents();

builder.Services.AddSingleton<RabbitMqService>(
    provider =>
        new RabbitMqService("hxc_queue", provider)
            .Receive<CreateOrganizationTodoCommand>());

builder.Host.UseSerilog(loggerConfiguration.CreateLogger());

var app = builder.Build();


var userTodos = new Todo[]
{
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateTime.Now),
    new(3, "Do the laundry", DateTime.Now.AddDays(1)),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateTime.Now.AddDays(2))
};
var apiMapGroup = app.MapGroup("api/");
var todosApi = apiMapGroup.MapGroup("user/todos");

todosApi.MapGet("/", () => userTodos);

todosApi.MapGet("/exception", () =>
{
    int i = 1;
    int j = 0;
    return i / j;
});

todosApi.MapGet("/exception2", () =>
{
    Guid todoExceptionId = Guid.NewGuid();
    throw new TodoException(todoExceptionId, nameof(Program));
});

todosApi.MapGet("/{id:int}", (int id) =>
    userTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

var organizationTodosApi =
    apiMapGroup.MapGroup("organization/todos").RequireAuthorization("organization");

organizationTodosApi.MapGet("/",
    async([FromServices] IGetOrganizationTodosQueryHandler queryHandler) =>
        await queryHandler.Handle(new GetOrganizationTodosQuery()));

organizationTodosApi.MapGet("/{todoId:int}",
    async([FromServices] IGetOrganizationTodosQueryHandler queryHandler, [FromRoute] int todoId) =>
        (await queryHandler.Handle(new GetOrganizationTodosQuery
        {
            TodoId = todoId
        })).Single());

organizationTodosApi.MapPost("/",
    async ([FromServices] ICreateOrganizationTodoCommandHandler commandHandler, [FromBody] Todo todo, ClaimsPrincipal user) =>
    {
        todo.OrganizationId = user.GetUserId();
        await commandHandler.HandleAsync(new CreateOrganizationTodoCommand(todo));
    });

app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.UseKnownExceptionMiddleware().UseSerilogRequestLogging();

app.Run();

[JsonSerializable(typeof(IEnumerable<Todo>))]
[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(ErrorLogEvent))]
[JsonSerializable(typeof(CreateOrganizationTodoCommand))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;