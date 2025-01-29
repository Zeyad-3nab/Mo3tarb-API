using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mo3tarb.Core.Entites.Identity;

namespace Mo3tarb.Repository.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var User = new AppUser
				{
					FirstName = "Mohamed",
					LastName="Bassem",
					UserName = "Mohamed.Bassem",
					Email = "mohammedatta095@gmail.com",
					PhoneNumber = "01094108780",
					WebsiteURL="Test",
					NationalId="12365478963214",
					WhatsappNumber= "01094108780",
					Type="Admin"
                };

				await userManager.CreateAsync(User, "Pa$$W0rd");
			}
		}
	}
}
