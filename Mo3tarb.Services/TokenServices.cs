using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Services
{
	public class TokenServices : ITokenService
	{
		private readonly IConfiguration _config;

		public TokenServices(IConfiguration config)
		{
			_config = config;
		}
		public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			var authClaim = new List<Claim>()
			{
				// Claim => User لل property هيا عباره عن شويه

				new Claim(ClaimTypes.NameIdentifier , user.Id),
				new Claim(ClaimTypes.GivenName , user.FirstName),
				new Claim(ClaimTypes.Email , user.Email)
			};

			var userRoles = await userManager.GetRolesAsync(user);

			foreach (var role in userRoles)
			{
				authClaim.Add(new Claim(ClaimTypes.Role, role));

			}

			// Key
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));

			// Token
			var Token = new JwtSecurityToken(
				issuer: _config["JWT:Issuer"],
				audience: _config["JWT:Audience"],
				expires: DateTime.Now.AddDays(double.Parse(_config["JWT:DurationInDays"])),
				claims: authClaim,
				 signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
			);
			return new JwtSecurityTokenHandler().WriteToken(Token);
		}
	}
}
