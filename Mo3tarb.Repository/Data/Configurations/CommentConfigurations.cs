using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mo3tarb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Data.Configurations
{
    public class CommentConfigurations : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(e => e.Apartment)
               .WithMany()
               .HasForeignKey(e => e.ApartmentId).IsRequired();

            builder.HasOne(e => e.User)
               .WithMany()
               .HasForeignKey(e => e.UserId).IsRequired();

            builder.Property(c => c.Text).IsRequired().HasMaxLength(500);
        }
    }
}