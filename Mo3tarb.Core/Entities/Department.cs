using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mo3tarb.Core.Entites
{
    public class Department:BaseEntitiy
    {
        [Required]
        public string Name { get; set; }

        //Location
        public List<AppUser> ApplicationUsers { get; set; } = new();
    }
}



