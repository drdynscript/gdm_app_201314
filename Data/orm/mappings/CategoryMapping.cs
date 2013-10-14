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
    internal class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
            : base()
        {
            this.ToTable("categories", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("category_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name).HasColumnName("category_name").IsRequired().HasMaxLength(128);
            this.Property(t => t.Description).HasColumnName("category_description").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("category_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.ModifiedDate).HasColumnName("category_modified").IsOptional();
            this.Property(t => t.DeletedDate).HasColumnName("category_deleted").IsOptional();
            this.Property(t => t.ParentId).HasColumnName("category_parentid").IsOptional();

            //FOREIGN KEYS
            this.HasOptional(p => p.ParentCategory).WithMany(t => t.ChildCategories).HasForeignKey(f => f.ParentId);
        }        
    }
}
