using NLog.Web;
using RestaurantApiUdemyCS6;
using RestaurantApiUdemyCS6.Entities;
using RestaurantApiUdemyCS6.Middleware;
using RestaurantApiUdemyCS6.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


     // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();


// Add services to the container.

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeeder>();
// Jako ¿e wstrzykujemy referencje do mappera musimy dodaæ serwisy automapera
// do kontenera zale¿noœci 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
seeder.Seed();

if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API"));

app.UseAuthorization();

app.MapControllers();

app.Run();
