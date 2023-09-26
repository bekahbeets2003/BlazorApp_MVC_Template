using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NLog.Web;
using NLog.Extensions;
using BlazorApp_MVC.Utilities;
using BlazorApp_MVC.Interfaces;
using BlazorApp_MVC.DataRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add services to the container.
IConfiguration configuration = builder.Configuration;
builder.Services.AddSingleton(configuration);

string connectionString = configuration.GetConnectionString("Default");
builder.Services.AddSingleton(connectionString);
builder.Services.AddSingleton<Utility>();
builder.Services.AddSingleton<IDbDapper, DbDapper>();

builder.Services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.AddNLog("NLog.config");
});


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
