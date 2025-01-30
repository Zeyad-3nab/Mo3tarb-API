using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class FavouriteDTO
    {
        //[Required(ErrorMessage ="UserId is required")]
        //public string UserId { get; set; }

        [Required(ErrorMessage ="apartmentId is required")]
        public int apartmentId { get; set; }
    }
}