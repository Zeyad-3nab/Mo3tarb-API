using Mo3tarb.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class ReturnFavouriteDTO
    {
        public int apartmentId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public string? Village { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public int NumOfRooms { get; set; }
        public string BaseImageURL { get; set; }
        public ApartmentType Type { get; set; }
        public double DistanceByMeters { get; set; }
        public bool IsRent { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}
