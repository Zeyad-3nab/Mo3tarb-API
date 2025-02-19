using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class GetSanaieeDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NationalId { get; set; }
        public string WhatsappNumber { get; set; }
        public string? WebsiteURL { get; set; }
        public string Type { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
