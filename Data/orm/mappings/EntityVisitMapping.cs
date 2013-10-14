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
    internal class EntityVisitMapping : EntityTypeConfiguration<EntityVisit>
    {
        public EntityVisitMapping()
            : base()
        {
            this.ToTable("entityvisits", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("entityvisit_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.SessionId).HasColumnName("entityvisit_sessionid").IsRequired();
            this.Property(t => t.UserAgent).HasColumnName("entityvisit_useragent").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("entityvisit_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.UserId).HasColumnName("user_id").IsOptional();
            this.Property(t => t.EntityId).HasColumnName("entity_id").IsOptional();

            //FOREIGN KEYS
            this.HasOptional(p => p.User).WithMany(t => t.Visits).HasForeignKey(f => f.UserId);
            this.HasRequired(p => p.Entity).WithMany(t => t.Visits).HasForeignKey(f => f.EntityId);
        }        
    }
}
