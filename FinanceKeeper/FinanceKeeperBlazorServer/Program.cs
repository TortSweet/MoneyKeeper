using FinanceKeeperBlazorServer.Data;
using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services;
using FinanceKeeperBlazorServer.Services.Interfaces;
using Microsoft.Extensions.Options;
using Refit;

var configuration = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppRoute>(configuration.Configuration.GetSection(nameof(AppRoute)));
var apiRoute = builder.Services.BuildServiceProvider().GetService<IOptions<AppRoute>>()!.Value;


builder.Services.AddRefitClient<IFinanceKeeperApi>()
    .ConfigureHttpClient(c =>
    {
        if (apiRoute.ApiUrl != null) c.BaseAddress = new Uri(apiRoute.ApiUrl);
    });


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<IBaseServices<FinancialCategory>, FinancialCategoryService>();
builder.Services.AddTransient<IBaseServices<FinancialOperation>, FinancialOperationService>();
builder.Services.AddTransient<IMoneyReport, MoneyReportService>();

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
