using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.DTOs
{
	public class UserDto
	{
		public string UserName { get; set; }
		public string Role { get; set; }
		public string Email { get; set; }
        public string Token { get; set; }
	}
}
