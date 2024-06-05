using JwtTest.Interfaces;
using JwtTest.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using JwtTest.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var policyName = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                              .WithOrigins("http://localhost:3000")
                              .WithMethods("GET")
                              .AllowAnyHeader();
                      });
 });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

     // JWT Authentication
     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
             {
                 In = ParameterLocation.Header,
                 Description = "Please insert JWT token into field",
                 Name = "Authorization",
                 Type = SecuritySchemeType.ApiKey,
                 BearerFormat = "JWT",
                 Scheme = "Bearer"
             });

     c.AddSecurityRequirement(new OpenApiSecurityRequirement {
             {
                 new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     new string[] { }
             }});
 });

 builder.Services.AddStackExchangeRedisCache(options =>
 {
     options.Configuration = "localhost";
     options.InstanceName = "SampleInstance";
 });

 builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext")));

 builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ITokenService, TokenService>();

 builder.Services.AddAuthentication(options =>
 {
     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
 }).AddJwtBearer(o =>
 {
     o.TokenValidationParameters = new TokenValidationParameters
         {
             ValidIssuer = builder.Configuration["AppSettings:ValidIssuer"],
             ValidAudience = builder.Configuration["AppSettings:ValidAudience"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"])),
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = false,
             ValidateIssuerSigningKey = true
         };
 });
builder.Services.AddAuthorization();

var app = builder.Build();
var scope = app.Services.CreateScope();
var cache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
var users = dbContext.users.ToList();
foreach (var user in users)
 {
    var userJson = JsonSerializer.Serialize(user);
    await cache.SetStringAsync(user.username, userJson);
 }
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
 {
     app.UseSwagger();
     app.UseSwaggerUI();
 }


app.UseHttpsRedirection();
app.UseCors(policyName);

app.UseAuthorization();

app.MapControllers();
app.Run();
