using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Models;

namespace Mo3tarb.Core.Entities
{
    public class ChatMessage:BaseEntitiy
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public AppUser Sender { get; set; }

        public AppUser Receiver { get; set; }
    }
}
