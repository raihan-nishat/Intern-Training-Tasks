using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MiniTaskManagerApi.Mapping;
using MiniTaskManagerApi.Middleware;
using MiniTaskManagerApi.Models;
using MiniTaskManagerApi.Repositories;
using MiniTaskManagerApi.Services;
using MiniTaskManagerApi.Validators;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Swagger document
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Mini Task Manager API",
            Version = "v1"
        });


    // JWT Bearer Authentication
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description = "Enter: Bearer {your JWT token}"
        });


    // Swagger Authorization requirement
    options.AddSecurityRequirement(
        document =>
            new OpenApiSecurityRequirement
            {
                [
                    new OpenApiSecuritySchemeReference(
                        "Bearer",
                        document)
                ] = []
            });
});


builder.Services.AddAutoMapper(
    cfg => { },
    typeof(MappingProfile));


builder.Services.AddSingleton<
    IUserRepository,
    UserRepository>();

builder.Services.AddSingleton<
    ITaskRepository,
    TaskRepository>();


builder.Services.AddSingleton<
    ITokenService,
    TokenService>();



builder.Services.AddSingleton<
    PasswordHasher<User>>();

builder.Services.AddValidatorsFromAssemblyContaining<
    TaskCreateValidator>();


var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "Jwt:Key is missing from appsettings.json.");


builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
               
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)),

              
                ValidateIssuer = false,

                ValidateAudience = false,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
    });


builder.Services.AddAuthorization();


var app = builder.Build();


app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseMiddleware<RequestLoggingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();