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
    internal class MemberMapping : EntityTypeConfiguration<Member>
    {
        public MemberMapping()
            : base()
        {
            this.ToTable("members", "gdm");

            this.Property(t => t.Password).HasColumnName("member_password").IsRequired().IsFixedLength();
            this.Property(t => t.PasswordSalt).HasColumnName("member_salt").IsRequired().IsFixedLength();
            this.Property(t => t.PasswordChangedDate).HasColumnName("member_passwordchanged").IsOptional();
            this.Property(t => t.PasswordFailuresSinceLastSucces).HasColumnName("member_loginfailurecount").IsOptional();
            this.Property(t => t.PasswordLastFailureDate).HasColumnName("member_loginfailed").IsOptional();
            this.Property(t => t.ConfirmationToken).HasColumnName("member_token").IsOptional().IsFixedLength();
            this.Property(t => t.ConfirmedDate).HasColumnName("member_confirmed").IsOptional();
        }        
    }
}
