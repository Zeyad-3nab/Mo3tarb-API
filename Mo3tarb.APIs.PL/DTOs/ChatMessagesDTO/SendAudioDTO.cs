using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs.ChatMessagesDTO
{
    public class SendAudioDTO
    {
        [Required(ErrorMessage = "ReciverId is required")]
        public string ReceiverId { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }
    }
}
