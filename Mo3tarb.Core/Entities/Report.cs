using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Entities
{
    public class Report:BaseEntitiy
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
