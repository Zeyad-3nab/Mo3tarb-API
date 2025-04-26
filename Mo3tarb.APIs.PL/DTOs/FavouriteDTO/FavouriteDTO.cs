using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs.FavouriteDTO
{
    public class FavouriteDTO
    {

        [Required(ErrorMessage = "apartmentId is required")]
        public int apartmentId { get; set; }
    }
}