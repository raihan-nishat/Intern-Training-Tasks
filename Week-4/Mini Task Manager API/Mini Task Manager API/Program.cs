using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Mini_Task_Manager_API.Data;
using Mini_Task_Manager_API.Mapping;
using Mini_Task_Manager_API.Middleware;
using Mini_Task_Manager_API.Repositories;
using Mini_Task_Manager_API.Repositories.Interfaces;
using Mini_Task_Manager_API.Services;
using Mini_Task_Manager_API.Services.Interfaces;
using MiniTaskManager.Validators;
using System.Text;
using Mini_Task_Manager_API.Models;


var builder = WebApplication.CreateBuilder(args);

// controller
builder.Services.AddControllers();


// swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});


// postgreSQL database connection

var connectionString =
    builder.Configuration.GetConnectionString(
        "DefaultConnection"
    );

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "PostgreSQL connection string is not configured."
    );
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});


// repositories and services

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// token srvice

builder.Services.AddScoped<ITokenService, TokenService>();

// auto mapper

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfiles>();
});

// fluent validation

builder.Services.AddValidatorsFromAssemblyContaining<TaskCreateDtoValidator>();


// jwt authentication

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException(
        "JWT key is not configured."
    );
}

if (string.IsNullOrWhiteSpace(jwtIssuer))
{
    throw new InvalidOperationException(
        "JWT issuer is not configured."
    );
}

if (string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException(
        "JWT audience is not configured."
    );
}

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme
    )
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)
                    ),

                ValidateIssuer = true,

                ValidIssuer = jwtIssuer,

                ValidateAudience = true,

                ValidAudience = jwtAudience,

                ValidateLifetime = true
            };
    });


// authorization

builder.Services.AddAuthorization();

// build the app

var app = builder.Build();

// database migration and seeding

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<AppDbContext>();

    db.Database.Migrate();

    var userRepository = services.GetRequiredService<IUserRepository>();

    var admin = userRepository.GetByUsername("admin");

    if (admin == null)
    {
        var passwordHasher =
            new Microsoft.AspNetCore.Identity.PasswordHasher<User>();

        var adminUser = new User
            {
            username = "admin",
            role = "Admin"
            };

        adminUser.passwordHash =
            passwordHasher.HashPassword(
                adminUser,
                "admin123"
            );

        userRepository.Add(adminUser);
    }
}

// middleware

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

// swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// https redirection

app.UseHttpsRedirection();

// authentication and authorization

app.UseAuthentication();
app.UseAuthorization();

// controllers

app.MapControllers();

// run the app

app.Run();
