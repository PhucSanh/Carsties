using AuctionService.Data;
using AuctionService.Data.SeedData.Auction;
using AuctionService.Repositories;
using AuctionService.Repositories.Generic;
using AuctionService.Services.Auction;
using Carsties.Shared.Excel.Service.Excel;
using Carsties.Shared.ExceptionHandler.Exceptions;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuctionSeedData, AuctionSeedData>();
// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();

//Services
builder.Services.AddScoped<IAuctionService, AuctionServiceimpl>();
builder.Services.AddScoped<IExcelService, ExcelService>();
var app = builder.Build();
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
try
{
    await DBInitializer.Initialize(app);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred during DB initialization: {ex.Message}");
}


app.Run();
