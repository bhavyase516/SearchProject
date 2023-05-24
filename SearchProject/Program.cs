using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SearchProject.BAL;
using SearchProject.DAL;
using SearchProject.DAL.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDbConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRectangleService, RectangleService>();
builder.Services.AddScoped<IRectangleRepository, RectangleRepository>();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//search endpoint
app.MapPost("/api/rectangle/search", async (IRectangleService rectangleService, List<Coordinate> coordinates) =>
{
    List<Rectangle> matchingRectangles = await rectangleService.SearchRectangles(coordinates);
    return Results.Ok(matchingRectangles);
});

//seeding logic
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Seed 200 rectangles
    var rectangleRepository = services.GetRequiredService<IRectangleRepository>();
    var rectangleSeeder = new RectangleSeeder(rectangleRepository);
    await rectangleSeeder.SeedAsync(200);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Retrieve the JWT secret key from appsettings.json
var jwtSecret = builder.Configuration["AppSettings:JwtSecret"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            //commenting valid issue and audience as this is demo project.
            //ValidIssuer = "",
            // ValidAudience = "",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });
app.UseHttpsRedirection();

app.Run();
