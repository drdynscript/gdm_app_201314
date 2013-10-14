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
    internal class InfoContactMapping : EntityTypeConfiguration<InfoContact>
    {
        public InfoContactMapping()
            : base()
        {
            this.ToTable("infocontacts", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("infocontact_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name).HasColumnName("guest_name").IsOptional().HasMaxLength(45);
            this.Property(t => t.Email).HasColumnName("guest_email").IsOptional().HasMaxLength(255);
            this.Property(t => t.UserAgent).HasColumnName("guest_useragent").IsOptional();
            this.Property(t => t.UserId).HasColumnName("user_id").IsOptional();
            this.Property(t => t.Subject).HasColumnName("infocontact_subject").IsRequired();
            this.Property(t => t.Body).HasColumnName("infocontact_body").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("infocontact_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.RepliedDate).HasColumnName("infocontact_replied").IsOptional();

            this.HasOptional(t => t.User).WithMany().HasForeignKey(t => t.UserId);
        }        
    }
}
