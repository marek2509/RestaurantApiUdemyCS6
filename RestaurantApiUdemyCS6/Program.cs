using RestaurantApiUdemyCS6;
using RestaurantApiUdemyCS6.Entities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

/*
     // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

 */

// Add services to the container.

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeeder>();

// Jako �e wstrzykujemy referencje do mappera musimy doda� serwisy automapera
// do kontenera zale�no�ci 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
seeder.Seed();
if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
