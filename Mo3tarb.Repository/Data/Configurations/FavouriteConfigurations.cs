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
    public class FavouriteConfigurations : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> builder)
        {
                builder.HasKey(f => new { f.UserId, f.apartmentId });

                builder.HasOne(f => f.apartment)
                       .WithMany() // لأن الشقة ممكن تكون في المفضلة عند أكتر من واحد
                       .HasForeignKey(f => f.apartmentId);


                builder.HasOne(f => f.User)
                       .WithMany()
                       .HasForeignKey(f => f.UserId);
        }
    }
}
