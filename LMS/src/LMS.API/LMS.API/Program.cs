using LMS.Application;
using LMS.Domain.Identity;
using LMS.Infrastructure;
using LMS.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace LMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ���������� �������� � ���������
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // ��������� Swagger � JWT ����������
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

            // ��������� JWT
            var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

            if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.SecretKey) ||
                string.IsNullOrEmpty(jwtOptions.Issuer) || string.IsNullOrEmpty(jwtOptions.Audience) ||
                jwtOptions.ExpirationInMinutes <= 0)
            {
                throw new InvalidOperationException("JWT options are not properly configured.");
            }

            // �������� ��������� ����
            var secretKey = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);

            // ����������� ��������������
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; // � ���������� ������� true
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ClockSkew = TimeSpan.Zero // ������� ��������� ������
                };
            });

            // ����������� �����������
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            });

            // ��������� CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            // ����������� �������������� ��������
            builder.Services.AddApplication();
            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // ������������ ��������� HTTP ��������
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
}