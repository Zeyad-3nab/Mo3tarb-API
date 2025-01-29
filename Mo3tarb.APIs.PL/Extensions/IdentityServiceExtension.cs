using Microsoft.AspNetCore.Identity;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Repository.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Mo3tarb.Core.Services;
using Mo3tarb.Services;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Repositories;
using Mo3tarb.Core.Models;
using Mo3tarb.APIs.PL.Extensions;

namespace Talabat.API.Extensions
{
    public static class IdentityServiceExtension
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddScoped<ITokenService,TokenServices>();
			services.AddScoped<IApartmentRepository,ApartmentRepository>();
			services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            //Add Swagger Extention
            services.AddSwaggerGenJwtAuth();

            //Add Custom Extention
            services.AddCustomJwtAuth(configuration);




            //services.AddAuthentication(Options =>
            //{
            //	//Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //	Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Bearer

            //})
            //	     .AddJwtBearer(Options =>
            //		 {
            //			 Options.TokenValidationParameters = new TokenValidationParameters()
            //			 {
            //				 // بتاعي Token اللي هيفلديت عليها عشان يعدي ال  Validation دي ال

            //				 ValidateIssuer = true,
            //				 ValidIssuer = configuration["JWT:ValidIssuer"],
            //				 ValidateAudience = true,
            //				 ValidAudience = configuration["JWT:ValidAudience"],
            //				 ValidateLifetime = true,
            //				 ValidateIssuerSigningKey = true,
            //				 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
            //			 };
            //		 }); 

            services.AddIdentity<AppUser, IdentityRole>()
							.AddEntityFrameworkStores<AppIdentityDbContext>();
			return services;

		}

	}
}
