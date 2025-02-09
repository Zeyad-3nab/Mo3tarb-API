using Mo3tarb.Core.Models;
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
    public class Comment:BaseEntitiy
    {

        public string UserId { get; set; }

        public int ApartmentId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Apartment Apartment { get; set; }
        public AppUser User { get; set; }
    }
}