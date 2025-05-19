using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mo3tarb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Data.Configurations
{
    public class ApartmentConfigurations : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)  //builder as a apartment
        {
            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(p => p.User)         //Relation With User
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ❌ منع الحذف التلقائي;

            builder.Property(p => p.Location).IsRequired();

            builder.Property(p => p.Price).IsRequired();

            builder.Property(p => p.DistanceByMeters).IsRequired();

            builder.Property(p => p.BaseImageURL).IsRequired();

            builder.Property(p => p.Type).IsRequired();

            builder.Property(p => p.IsRent).IsRequired();

            builder.Property(p => p.DateOfCreation).IsRequired();

            builder.Property(p => p.UserId).IsRequired();
        }
    }
}