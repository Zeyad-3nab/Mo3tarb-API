using Mo3tarb.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Models
{
    public class Apartment:BaseEntitiy
    {
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

        [Required]
        public string UserId { get; set; }
        //public AppUser User { get; set; }

        public List<string>? ImagesURL { get; set; } = new List<string>();  //Images
    }
}
