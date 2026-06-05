


using Carsties.Shared.ExceptionHandler.Exceptions;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumer;
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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddHttpClient<IAuctionSvc, AuctionSvcHttpClient>().AddPolicyHandler(GetRetryPolicy());
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await DBInitializer.InitializeAsync(app);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing database: {ex.Message}");
    }

});



app.Run();


// static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
//  => HttpPolicyExtensions
//     .HandleTransientHttpError()
//     .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
//     .WaitAndRetryForeverAsync(x => TimeSpan.FromSeconds(3));

