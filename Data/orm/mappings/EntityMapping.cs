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
    internal class EntityMapping : EntityTypeConfiguration<Entity>
    {
        public EntityMapping()
            : base()
        {
            this.ToTable("entities", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("entity_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Title).HasColumnName("entity_title").IsRequired().HasMaxLength(128);
            this.Property(t => t.Description).HasColumnName("entity_description").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("entity_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.ModifiedDate).HasColumnName("entity_modified").IsOptional();
            this.Property(t => t.DeletedDate).HasColumnName("entity_deleted").IsOptional();
            this.Property(t => t.UserId).HasColumnName("user_id").IsRequired();
            this.Property(t => t.CanBeVisitByGuests).HasColumnName("entity_ispublic").IsRequired();

            //FOREIGN KEYS
            this.HasRequired(p => p.User).WithMany(t => t.Entities).HasForeignKey(f => f.UserId).WillCascadeOnDelete();

            //MANYTOMANY RELATIONSHIPS
            this.HasMany(p => p.Categories)
                .WithMany(t => t.Entities)
                .Map(mc =>
                {
                    mc.ToTable("entities_has_categories");
                    mc.MapLeftKey("entity_id");
                    mc.MapRightKey("category_id");
                });
            this.HasMany(p => p.Likes)
                .WithMany(t => t.Likes)
                .Map(mc =>
                {
                    mc.ToTable("entities_has_likes");
                    mc.MapLeftKey("entity_id");
                    mc.MapRightKey("user_id");
                });
        }        
    }
}
