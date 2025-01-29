﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Mo3tarb.APIs.PL.Extensions
{
    public static class CustomJWTAuthExtention
    {
        public static void AddCustomJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))

                };

            });

        }



        public static void AddSwaggerGenJwtAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(O =>
            {
                O.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Mo3tarib API",
                    Description = "Output",
                    Contact = new OpenApiContact()
                    {
                        Name = "",
                        Email = "mohammedatta095@gmail.com",
                        Url = new Uri("https://wa.me/+201094108780")

                    }


                });

                O.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter The JWT Token"

                });

                O.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                           new OpenApiSecurityScheme()
                       {
                           Reference=new OpenApiReference()
                           {
                               Type=ReferenceType.SecurityScheme,
                               Id="Bearer"
                           },
                           Name = "Bearer",
                           In=ParameterLocation.Header
                       },
                       new List<string>()
                    }

                });
            });

        }
    }
}
