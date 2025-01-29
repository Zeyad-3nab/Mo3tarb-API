﻿using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.DTOs
{
	public class LoginDto
	{
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
