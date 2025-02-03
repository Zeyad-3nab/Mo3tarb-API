using Microsoft.AspNetCore.Mvc;
using Mo3tarb.Core.Repositries;
using Mo3tarb.APIs.Errors;
using Mo3tarb.Core.Services;
using Mo3tarb.Repository;
using Mo3tarb.Repository.Data;
using Mo3tarb.Repository.Repositories;
using Mo3tarb.Core.Models;
using AutoMapper;
using GraduationProject.API.PL.Mapping;
using Mo3tarb.Core.Entites;
using Mo3tarb.APIs.PL.Errors;
using Mo3tarb.APIs.PL.Extensions;
using Mo3tarb.Services;
using Microsoft.AspNetCore.Identity;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Repository.Identity;

namespace Mo3tarb.APIs.Extensions
{
    public static class ApplicationServicesEvtention
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection Services , IConfiguration configuration)
		{
			Services.AddScoped<IChatRepository, ChatRepository>();
            Services.AddScoped<ITokenService, TokenServices>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services.AddScoped<IApartmentRepository, ApartmentRepository>();
			//Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			//Services.AddScoped<IFavouriteRepository, FavouriteRepository>();
			//Services.AddScoped<ICommentRepository, CommentRepository>();
			//Services.AddScoped<IRatingRepository, RatingRepository>();
			//Services.AddScoped<IReportRepository, ReportRepository>();


            Services.AddAutoMapper(M => M.AddProfile(new Applicationprofile(configuration)));

            Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>();
            Services.AddSignalR();


            return Services;

		}
	}
}
