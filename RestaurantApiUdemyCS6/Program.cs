using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using RestaurantApiUdemyCS6;
using RestaurantApiUdemyCS6.Authorization;
using RestaurantApiUdemyCS6.Entities;
using RestaurantApiUdemyCS6.Middleware;
using RestaurantApiUdemyCS6.Models;
using RestaurantApiUdemyCS6.Models.Validator;
using RestaurantApiUdemyCS6.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


// Add services to the container.

//builder.Services.AddEndpointsApiExplorer();
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false; 
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer, // wydawca tokenu
        ValidAudience = authenticationSettings.JwtIssuer, // kto mo¿e korzystaæ z tokenu, ta sama wartoœæ bo korzstamy w obrêbie naszej aplikacji
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(authenticationSettings.JwtKey)), //kluczy prywatny wygenerowany na podstawie JwtKey zapisanej w appsettings.json
    };
});

builder.Services.AddAuthorization(option => {
    option.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "German", "Polish"));
    option.AddPolicy("Atleast20",
        builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
});

builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeReqiurementHandler>();
//builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeeder>();
// Jako ¿e wstrzykujemy referencje do mappera musimy dodaæ serwisy automapera
// do kontenera zale¿noœci 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

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
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API"));



app.UseAuthorization();

app.MapControllers();

app.Run();
