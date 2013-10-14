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
    internal class NewsletterSubscriptionMapping : EntityTypeConfiguration<NewsletterSubscription>
    {
        public NewsletterSubscriptionMapping()
            : base()
        {
            this.ToTable("newslettersubscriptions", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("newslettersubscription_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Email).HasColumnName("guest_email").IsOptional().HasMaxLength(255);
            this.Property(t => t.UserAgent).HasColumnName("guest_useragent").IsRequired();
            this.Property(t => t.UserId).HasColumnName("user_id").IsOptional();
            this.Property(t => t.CreatedDate).HasColumnName("newslettersubscription_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            this.HasOptional(t => t.User).WithMany().HasForeignKey(t => t.UserId);
        }        
    }
}
