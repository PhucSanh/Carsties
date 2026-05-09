


using Carsties.Shared.ExceptionHandler.Exceptions;
using SearchService.Data;
using SearchService.Repositories;
using SearchService.Services.AuctionSvc;
using SearchService.Services.Search;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ISearchService, SearchServiceImpl>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

builder.Services.AddHttpClient<IAuctionSvc, AuctionSvcHttpClient>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

try
{
    await DBInitializer.InitializeAsync(app);
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing database: {ex.Message}");
}

app.Run();

