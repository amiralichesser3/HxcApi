using HxcApiJit.Ef;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Development.Sensitive.json", optional: true, reloadOnChange: true); 
builder.Configuration.AddJsonFile("appsettings.Production.Sensitive.json", optional: true, reloadOnChange: true); 

string readConString = builder.Configuration["ConnectionStrings:HxcDb_Read"]!;
string writeConString = builder.Configuration["ConnectionStrings:HxcDb_Write"]!;
builder.Services.AddRazorPages();

builder.Services.AddDbContext<HxcReadDbContext>(options => options.UseSqlServer(readConString));
builder.Services.AddDbContext<HxcWriteDbContext>(options => options.UseSqlServer(writeConString));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();