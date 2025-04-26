namespace Mo3tarb.APIs.PL.DTOs.ChatMessagesDTO
{
    public class ReturnChat
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
