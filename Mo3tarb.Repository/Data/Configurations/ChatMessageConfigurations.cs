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
    public class ChatMessageConfigurations : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasOne(e => e.Receiver)
               .WithMany()
               .HasForeignKey(e => e.ReceiverId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Sender)
              .WithMany()
              .HasForeignKey(e => e.SenderId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e=>e.SenderId).IsRequired();
            builder.Property(e=>e.ReceiverId).IsRequired();
            builder.Property(e=>e.Timestamp).IsRequired();
            builder.Property(e=>e.Message).IsRequired();

        }
    }
}
