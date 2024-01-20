using System.Security.Claims;
using System.Text.Json.Serialization;
using Dapper;
using HxcApi.DataAccess.Contracts.Todos.Queries;
using HxcApi.DataAccess.DapperImplementation.Todos.Ioc;
using HxcCommon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

[module:DapperAot]

Environment.SetEnvironmentVariable("CORECLR_GLOBAL_INVARIANT", "1");

var builder = WebApplication.CreateSlimBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Development.Sensitive.json", optional: true, reloadOnChange: true); 
builder.Configuration.AddJsonFile("appsettings.Production.Sensitive.json", optional: true, reloadOnChange: true); 
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

string authority = builder.Configuration["Auth0:Domain"]!;
string audience = builder.Configuration["Auth0:Audience"]!;
string hxcConString = builder.Configuration["ConnectionStrings:HxcDb"]!;
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

builder.Services.AddScoped<SqlConnection>(_ => new SqlConnection(hxcConString));

builder.Services.RegisterTodoQueries();

var app = builder.Build();

var userTodos = new Todo[]
{
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};
var apiMapGroup = app.MapGroup("api/");
var todosApi = apiMapGroup.MapGroup("user/todos");

todosApi.MapGet("/", () => userTodos);

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

app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.Run();

[JsonSerializable(typeof(IEnumerable<Todo>))]
[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;