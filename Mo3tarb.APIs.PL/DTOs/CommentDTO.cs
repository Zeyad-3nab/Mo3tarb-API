using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="ApartmentId is required")]

        public int ApartmentId { get; set; }

        [Required(ErrorMessage ="Text is required")]
        [MaxLength(500 , ErrorMessage ="Maximim length of text is 500")]
        public string Text { get; set; }
    }
}