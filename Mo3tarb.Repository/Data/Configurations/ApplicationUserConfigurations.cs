using Mo3tarb.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mo3tarb.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.API.DAL.Data.Configurations
{
    public class ApplicationUserConfigurations :IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId).IsRequired(false);

            builder.HasIndex(u => u.NationalId).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique(true);
            builder.Property(u => u.NationalId).HasMaxLength(14).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.WhatsappNumber).IsRequired();
            //builder.Property(p => p.WebsiteURL).IsRequired(false);
            builder.Property(p => p.DepartmentId).IsRequired(false);
        }
    }
}
