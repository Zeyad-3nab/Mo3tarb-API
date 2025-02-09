using Mo3tarb.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class UpdateApartmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        public string? Village { get; set; }


        [Required(ErrorMessage = "Location is required")]
        [DataType(DataType.Url, ErrorMessage = "Location Sould be URL")]
        public string Location { get; set; }


        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }


        public IFormFile? BaseImage { get; set; }


        [Required(ErrorMessage = "NumberOfRooms is required")]
        public int NumOfRooms { get; set; }



        [Required(ErrorMessage = "Type is required")]
        public ApartmentType Type { get; set; }

        public double address_Lat { get; set; }
        public double address_Lon { get; set; }



        [Required(ErrorMessage = "IsRent or not IsRequired")]
        public bool IsRent { get; set; }
    }
}
