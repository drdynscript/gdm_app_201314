using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.orm.mappings
{
    internal class MessageMapping : EntityTypeConfiguration<Message>
    {
        public MessageMapping()
            : base()
        {
            this.ToTable("messages", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("message_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Body).HasColumnName("message_body").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("message_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.DeletedDate).HasColumnName("message_deleted").IsOptional();
            this.Property(t => t.SenderId).HasColumnName("user_id").IsRequired();
            this.Property(t => t.ReceiverId).HasColumnName("friend_id").IsRequired();

            //FOREIGN KEYS
            this.HasRequired(p => p.Sender).WithMany(t => t.SendedMessages).HasForeignKey(f => f.SenderId);
            this.HasRequired(p => p.Receiver).WithMany(t => t.ReceivedMessages).HasForeignKey(f => f.ReceiverId);
        }        
    }
}
