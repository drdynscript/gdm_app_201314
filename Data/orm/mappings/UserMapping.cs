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
    internal class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
            : base()
        {
            this.ToTable("users", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("user_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.UserName).HasColumnName("user_username").IsRequired().HasMaxLength(24);
            this.Property(t => t.Email).HasColumnName("user_email").IsRequired().HasMaxLength(255);
            this.Property(t => t.CreatedDate).HasColumnName("user_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.ModifiedDate).HasColumnName("user_modified").IsOptional();
            this.Property(t => t.DeletedDate).HasColumnName("user_deleted").IsOptional();
            this.Property(t => t.LockedDate).HasColumnName("user_locked").IsOptional();
            this.Property(t => t.LastLoggedinDate).HasColumnName("user_lastlogin").IsOptional();
            this.Property(t => t.PersonId).HasColumnName("person_id").IsRequired();

            //ONE-TO-ONE-TO-ZERO (UNIDIRECTIONAL)
            this.HasRequired(p => p.Person).WithMany().HasForeignKey(p => p.PersonId);

            //MANYTOMANY
            this.HasMany(p => p.Roles)
                .WithMany(t => t.Users)
                .Map(mc =>
                {
                    mc.ToTable("users_has_roles");
                    mc.MapLeftKey("user_id");
                    mc.MapRightKey("role_id");
                });
            this.HasMany(p => p.Friends)
                .WithMany(t => t.Users)
                .Map(mc =>
                {
                    mc.ToTable("users_has_friends");
                    mc.MapLeftKey("user_id");
                    mc.MapRightKey("friend_id");
                });
        }        
    }
}
