using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs.DepartmentDTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "DepartmentName is required")]
        public string Name { get; set; }
    }
}
