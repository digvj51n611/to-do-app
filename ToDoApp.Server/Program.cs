using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoApp.data;
using ToDoApp.Data.IRepos;
using ToDoApp.Data.Repos;
using ToDoApp.Data.Validators;
using ToDoApp.Server.Authentication;
using ToDoApp.Server.Services;
using ToDoApp.Service.IServices;
using ToDoApp.Service.Mappers;
using ToDoApp.Service.Models;
using ToDoApp.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using ToDoApp.Data.Entities;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee_Directory_API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
builder.Services.AddDbContext<ToDoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpContextAccessor();
builder.Services
    .AddScoped<IGenericRepo<User>,IGenericRepo<User>>()
    .AddScoped<IGenericRepo<TaskItem>,GenericRepo<TaskItem>>()
    .AddScoped<IUserRepo, UserRepo>()
    .AddScoped<ITaskItemRepo, TaskItemRepo>()
    .AddScoped<IValidator<TaskDto>, TaskDtoValidator>()
    .AddScoped<IValidator<UserDto>, UserDtoValidator>()
    .AddScoped((provider) =>
    {
        var config = builder.Configuration.GetSection("Jwt");
        return new AuthConfig(config["Key"]!, config["Issuer"]!, config["Audience"]!);
    })
    .AddScoped<IUserService, UserService>()
    .AddScoped<ITaskItemService, TaskItemService>()
    .AddScoped<IAuthentication, AuthenticationService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
    };
});
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
