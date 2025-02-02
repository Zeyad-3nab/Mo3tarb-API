using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mo3tarb.Core.Entites.Identity;

namespace Mo3tarb.Core.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public AppUser Sender { get; set; }

        public AppUser Receiver { get; set; }
    }
}
