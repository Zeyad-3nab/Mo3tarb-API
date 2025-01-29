using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.DTOs
{
	public class RegisterDto
	{

		[Required (ErrorMessage ="UserName is required")]
		public string UserName { get; set; }


        [Required(ErrorMessage = "FirstName is required")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "NationalId is required")]
        [MaxLength(14, ErrorMessage = "MaxLength of NationalId is 14")]
        [MinLength(14, ErrorMessage = "MinLength of NationalId is 14")]
        public string NationalId { get; set; }

        //public IFormFile? Photo { get; set; }

        [Required(ErrorMessage = "WhatsappNumber is required")]
        public string WhatsappNumber { get; set; }

        public string? WebsiteURL { get; set; }

        [Required(ErrorMessage ="Type of user is required")]
        public string Type { get; set; }

        public int? DepartmentId { get; set; }

        [Required(ErrorMessage ="Phone Number is required")]
		[Phone(ErrorMessage="PhoneNumber should be data Type Of phone")]
		public string PhoneNumber { get; set; }
    }
}
