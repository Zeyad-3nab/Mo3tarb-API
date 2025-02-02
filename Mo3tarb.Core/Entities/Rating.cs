using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Entities
{
    public class Rating
    {
        public string UserId { get; set; } // المستخدم اللي عمل تقيم	
        public int ApartmentId { get; set; } // الشقة التي بيتم تقييمها
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Score { get; set; } // قيمة التقييم مثلاً من 1 إلى 5
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public AppUser User { get; set; }
        public Apartment Apartment { get; set; }

    }
}
