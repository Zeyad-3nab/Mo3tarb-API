using System.ComponentModel.DataAnnotations.Schema;

namespace Mo3tarb.APIs.DTOs
{
	public class AddressDto
	{
        public int Id { get; set; }
        public string Fname { get; set; }
		public string Lname { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string Country { get; set; }
	
	}
}
