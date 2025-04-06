using LMS.API.Middleware;
using LMS.Application;
using LMS.Domain.Identity;
using LMS.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using Quartz;
namespace LMS.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddQuartz(q =>
        {
            var jobKey = new JobKey("RabbitMqToPostgresJob");


            q.AddJob<QueriesToPsql>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
           .ForJob(jobKey)
           .WithIdentity("RabbitMqToPostgresTrigger")
           .WithCronSchedule("0 51 17 * * ?"));
        });

        builder.Services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LMS API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
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

        // Настройка JWT
        var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

        if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.SecretKey) ||
            string.IsNullOrEmpty(jwtOptions.Issuer) || string.IsNullOrEmpty(jwtOptions.Audience) ||
            jwtOptions.ExpirationInMinutes <= 0)
        {
            throw new InvalidOperationException("JWT options are not properly configured.");
        }

        // Кодируем секретный ключ
        var secretKey = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);

        // Настраиваем аутентификацию
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false; // В продакшене поставь true
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ClockSkew = TimeSpan.Zero // убираем временной допуск
            };
        });

        // Настраиваем авторизацию
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
            options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            options.AddPolicy("Teacher", policy => policy.RequireClaim(ClaimTypes.Role, "Teacher"));
        });

        // Настройка CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });

        // Регистрация дополнительных сервисов
        builder.Services.AddApplication();
        builder.Services.AddDataAccess(builder.Configuration);
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        // Конфигурация конвейера HTTP запросов
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAuthorization();
        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<PerformanceMiddleware>();
    
        //app.UseMiddleware<UserIdMiddleware>();
        app.MapControllers();

        app.Run();
    }
}

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; } 
}