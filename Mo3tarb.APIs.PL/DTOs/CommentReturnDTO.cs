using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.APIs.PL.DTOs
{
    public class CommentReturnDTO
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string UserId { get; set; }

        public string Text { get; set; }
        public string UserName { get; set; }

    }
}
