using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Data.Configurations
{
    public class ReportConfigurations : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
           builder.HasOne(e => e.User)
           .WithMany()
           .HasForeignKey(e => e.UserId).IsRequired(false);

            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.Text).IsRequired();
        }
    }
}
