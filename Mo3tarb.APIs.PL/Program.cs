using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Core.Models;
using Mo3tarb.Repository.Data;
using Mo3tarb.Repository.Repositories;
using Mo3tarb.Extensions;
using Mo3tarb.APIs.Extensions;
using Mo3tarb.Repository.Identity;
using Mo3tarb.APIs.PL.Extensions;
using Mo3tarb.Repository.RealTime;

public class Program() 
{
    public static async Task Main(string[] args) 
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);


        //Allow all people
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.AllowAnyOrigin();
                                  policy.AllowAnyMethod();
                                  policy.AllowAnyHeader();
                              });
        });

        // Add services to the container.

        #region Configure Service =>  Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));   //Scoped object ber request
        });

		builder.Services.AddDbContext<ApplicationDbContext>(Options =>
		{
			Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

		});

        builder.Services.AddApplicationServices(builder.Configuration);


        //Add Swagger Extention
        builder.Services.AddSwaggerGenJwtAuth();

        //Add Custom Extention
        builder.Services.AddCustomJwtAuth(builder.Configuration);
       

        #endregion

        var app = builder.Build();


        #region Update-DataBase
        using var scope = app.Services.CreateScope();  //„Ã„Ê⁄Â «·”Ì—›Ì”Ì” «··Ì «··«Ì›  «Ì„ » «⁄Â« scoped
        var Services = scope.ServiceProvider;
        var logerFactory= Services.GetRequiredService<ILoggerFactory>();
        try
        {
            var DbContext = Services.GetRequiredService<ApplicationDbContext>();
            await DbContext.Database.MigrateAsync();
        }
        catch (Exception ex) 
        {
            var logger = logerFactory.CreateLogger<Program>();
            logger.LogError(ex ,"An Error occured during  Update Database");
        }
        #endregion

        #region Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
            app.UseSwaggerMiddelware();
		//}

		app.UseStatusCodePagesWithReExecute("/error/{0}"); //????? end point ? Redirect ????? ???? <= StatusCode ???? ???? ?

        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthorization();

        app.MapControllers();
        app.MapHub<ChatHub>("/chatHub");
        #endregion

        app.Run();

    }
}