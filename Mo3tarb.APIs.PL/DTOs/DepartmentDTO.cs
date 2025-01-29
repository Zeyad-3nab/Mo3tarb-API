using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.API.DTOs.DepartmentDTOs
{
	public class DepartmentDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="DepartmentName is required")]
		public string Name { get; set; }
	}
}
