using FinanceKeeper.AutoMapperProfiles;
using FinanceKeeper.Data;
using FinanceKeeper.Dtos;
using FinanceKeeper.Repositories;
using FinanceKeeper.Repositories.Abstracions;
using FinanceKeeper.Services;
using FinanceKeeper.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using FinanceKeeper.Middleware;


//TODO: Try to use ICollection pattern (Pattern matching)

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
    builder.Services.AddAutoMapper(typeof(FinancialCategoryProfile));
    builder.Services.AddAutoMapper(typeof(FinancialOperationProfile));

    builder.Services.AddControllers();
    builder.Services.AddTransient<IBaseServices<FinancialCategoryDto>, FinancialCategoryService>();
    builder.Services.AddTransient<IBaseServices<FinancialOperationDto>, FinancialOperationService>();
    builder.Services.AddTransient<IMoneyReport, MoneyReportService>();
    builder.Services.AddTransient<IFinancialCategoryRepository, FinancialCategoryRepository>();
    builder.Services.AddTransient<IFinancialOperationRepository, FinancialOperationRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(doc =>
    {
        doc.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Financial Keeper web api application",
            Description = "A simple ASP.NET Core Web Api for educational goals",
            Version = "v1"
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, xmlFile);

        doc.IncludeXmlComments(xmlPath);
    });


// Connection to Data Base
    builder.Services.AddDbContext<AppDbContext>(option =>
    {
        option.UseSqlServer(builder.Configuration.GetConnectionString("default"));
    });


    var app = builder.Build();

    app.UseCustomExceptionHandler();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
