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
    internal class RoleMapping : EntityTypeConfiguration<Role>
    {
        public RoleMapping()
            : base()
        {
            this.ToTable("roles", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("role_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name).HasColumnName("role_name").IsRequired().HasMaxLength(128);
            this.Property(t => t.Description).HasColumnName("role_description").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("role_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.ModifiedDate).HasColumnName("role_modified").IsOptional();
            this.Property(t => t.DeletedDate).HasColumnName("role_deleted").IsOptional();
        }        
    }
}
