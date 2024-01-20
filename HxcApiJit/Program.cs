using HxcApiJit.Ef;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Development.Sensitive.json", optional: true, reloadOnChange: true); 
builder.Configuration.AddJsonFile("appsettings.Production.Sensitive.json", optional: true, reloadOnChange: true); 

string hxcConString = builder.Configuration["ConnectionStrings:HxcDb"]!;
builder.Services.AddRazorPages();

builder.Services.AddDbContext<HxcDbContext>(options => options.UseSqlServer(hxcConString));


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