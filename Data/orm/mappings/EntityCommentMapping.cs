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
    internal class EntityCommentMapping : EntityTypeConfiguration<EntityComment>
    {
        public EntityCommentMapping()
            : base()
        {
            this.ToTable("entitycomments", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("comment_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Body).HasColumnName("comment_name").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("comment_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.ModifiedDate).HasColumnName("comment_modified").IsOptional();
            this.Property(t => t.DeletedDate).HasColumnName("comment_deleted").IsOptional();
            this.Property(t => t.LockedDate).HasColumnName("comment_locked").IsOptional();
            this.Property(t => t.ParentId).HasColumnName("comment_parentid").IsOptional();

            //FOREIGN KEYS
            this.HasRequired(p => p.User).WithMany(t => t.Comments).HasForeignKey(f => f.UserId);

            this.HasRequired(p => p.Entity).WithMany(t => t.Comments).HasForeignKey(f => f.EntityId);

            this.HasRequired(p => p.ParentComment).WithMany(t => t.ChildComments).HasForeignKey(f => f.ParentId);
        }        
    }
}
