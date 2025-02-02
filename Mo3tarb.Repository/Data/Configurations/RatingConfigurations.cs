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
    public class RatingConfigurations : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => new { r.UserId, r.ApartmentId });

            builder.HasOne(r => r.Apartment)
                   .WithMany() // لأن الشقة ممكن تكون في المفضلة عند أكتر من واحد
                   .HasForeignKey(r => r.ApartmentId).IsRequired();


            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId).IsRequired();
        }
    }
}
