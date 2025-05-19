using Mo3tarb.Core.Entities;

namespace Mo3tarb.APIs.PL.DTOs.ChatMessagesDTO
{
    public class ReturnChat
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public bool IsRead { get; set; }
        public MessageType MessageType { get; set; }
        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
