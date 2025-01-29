using Microsoft.AspNetCore.Mvc;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class ApartmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        public string? Village { get; set; }


        [Required(ErrorMessage = "Location is required")]
        [DataType(DataType.Url , ErrorMessage ="Location Sould be URL")]
        public string Location { get; set; }


        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        [Required (ErrorMessage ="Base Image is required")]
        public IFormFile BaseImage { get; set; }


        [Required(ErrorMessage = "NumberOfRooms is required")]
        public int NumOfRooms { get; set; }



        [Required(ErrorMessage = "Type is required")]
        public ApartmentType Type { get; set; }



        [Required(ErrorMessage = "address lat is required")]
        public double address_Lat { get; set; }



        [Required(ErrorMessage = "address lon is required")]
        public double address_Lon { get; set; }



        [Required(ErrorMessage ="IsRent or not IsRequired")]
        public bool IsRent { get; set; }
        public List<IFormFile>? Images { get; set; } = new List<IFormFile>();  //Images
    }
}
