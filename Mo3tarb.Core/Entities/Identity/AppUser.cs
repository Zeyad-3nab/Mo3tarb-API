using Microsoft.AspNetCore.Identity;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Entites.Identity
{
	public class AppUser: IdentityUser
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public string? PhotoURL { get; set; }

        public string WhatsappNumber { get; set; }
        public string? WebsiteURL { get; set; }
        public string NationalId { get; set; }
        public string Type { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Apartment> Apartments { get; set; } = new();

    }
}
