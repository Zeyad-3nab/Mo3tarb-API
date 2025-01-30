using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Entities
{
    public class Favourite
    {
        public string UserId { get; set; }
        public int apartmentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public AppUser User { get; set; }
        public Apartment apartment { get; set; }
    }
}
