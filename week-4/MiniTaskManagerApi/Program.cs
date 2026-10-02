using MiniTaskManagerApi.Repositories;
using MiniTaskManagerApi.Repositories.Interfaces;
using MiniTaskManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddSingleton<PasswordService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
