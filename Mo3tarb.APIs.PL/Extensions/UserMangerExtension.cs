using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Mo3tarb.Core.Entites.Identity;

namespace Mo3tarb.API.Extensions
{
	public static class UserMangerExtension
	{
		public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager ,ClaimsPrincipal user)
		{
			var Email = user.FindFirstValue(ClaimTypes.Email);
			var Users =await userManager.Users.FirstOrDefaultAsync(U => U.Email == Email);
			return Users;
		}
	}
}
