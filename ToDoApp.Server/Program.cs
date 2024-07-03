using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoApp.data;
using ToDoApp.Data.IRepos;
using ToDoApp.Data.Repos;
using ToDoApp.Data.Validators;
using ToDoApp.Service.Mappers;
using ToDoApp.Service.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
builder.Services.AddDbContext<ToDoDbContext>();
builder.Services
    .AddScoped<IUserRepo, UserRepo>()
    .AddScoped<ITaskItemRepo, TaskItemRepo>()
    .AddScoped<IValidator<TaskDto>,TaskDtoValidator>()
    .AddScoped<IValidator<UserDto>,UserDtoValidator>();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
