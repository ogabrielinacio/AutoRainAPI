using System.Reflection;
using SmartIrrigatorAPI.Data;
using SmartIrrigatorAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(
    new DatabaseUtils(builder.Configuration).GetConnectionString())
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("smart-irrigator-api-doc", new OpenApiInfo {
        Title = "SmartIrrigatorAPI",
        Description = "API for AutoRain Device",
        Version = "v1" 
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    
    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "Bearer Token Pattern; Example: \"Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        In = ParameterLocation.Header
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(AuthenticationUtils.GetSecretKey())
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/smart-irrigator-api-doc/swagger.json", "SmartIrrigatorAPI v1"); 
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();