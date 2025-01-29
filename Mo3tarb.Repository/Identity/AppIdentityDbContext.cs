using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Models;

namespace Mo3tarb.Repository.Identity
{
	public class AppIdentityDbContext : IdentityDbContext<AppUser>
	{
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
    }
}
