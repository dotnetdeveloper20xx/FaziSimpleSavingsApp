using Application.Common.Security;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Application Layer Setup
// ---------------------------
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("FaziSimpleSavings.Application")));

builder.Services.AddValidatorsFromAssembly(Assembly.Load("FaziSimpleSavings.Application"));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Application.Behaviors.ValidationBehavior<,>));

// ---------------------------
// Infrastructure & DbContext
// ---------------------------
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------------
// Authentication & Authorization
// ---------------------------
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<IOwnershipValidator, OwnershipValidator>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// ---------------------------
// Add Controllers (Required)
// ---------------------------
builder.Services.AddControllers();

// ---------------------------
// Swagger Configuration with JWT
// ---------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FaziSimpleSavings API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your JWT token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactClient", policy =>
    {
        policy.WithOrigins("http://192.168.0.182:3000", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // if using cookies or Authorization headers
    });
});


var app = builder.Build();

// Enable CORS
app.UseCors("AllowReactClient");


// ---------------------------
// Run Database Migrations and Seed Data
// ---------------------------
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // ensure DB is created
    await SeedData.SeedAsync(dbContext); // seed initial data
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

// ---------------------------
// HTTP Request Pipeline
// ---------------------------
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FaziSimpleSavings API v1");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // required to route attribute-based APIs

app.Run();
