using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class AddToRoleDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}