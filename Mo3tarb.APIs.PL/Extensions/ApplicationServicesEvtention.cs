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

namespace Mo3tarb.APIs.Extensions
{
    public static class ApplicationServicesEvtention
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection Services , IConfiguration configuration)
		{
			Services.AddScoped(typeof(IGenaricRepository<Apartment>), (typeof(ApartmentRepository)));
			Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            Services.AddAutoMapper(M => M.AddProfile(new Applicationprofile(configuration)));
           

            #region Error Handling
            Services.Configure<ApiBehaviorOptions>(Options =>
			{
				//Options.InvalidModelStateResponseFactory = (actionContext) =>
				//{
				//	// ModelState => Dicsanary [KeyValuePair]
				//	// Key => Name Of Param
				//	// Value => Error
				//	//ValidationError هنا بكريت شكل جديد من ال
				//	// Note :Project بيتعمل مره واحده بس في ال ValidationError ال
				//	var error = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
				//										.SelectMany(P => P.Value.Errors)
				//										.Select(E => E.ErrorMessage)
				//										.ToArray();

				//	var response = new ApiValidationResponse()
				//	{
				//		errors = error
				//	};
				//	return new BadRequestObjectResult(response);
				//};
			});

            #endregion

            ////Add Swagger Extention
            //Services.AddSwaggerGenJwtAuth();

            ////Add Custom Extention
            //Services.AddCustomJwtAuth(configuration);

            return Services;

		}
	}
}
