using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class RatingDTO
    {
        [Required(ErrorMessage = "ApartmentId is required")]
        public int ApartmentId { get; set; } // الشقة التي بيتم تقييمها
        [Required(ErrorMessage ="ScoreId is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Score { get; set; } // قيمة التقييم مثلاً من 1 إلى 5
    }
}