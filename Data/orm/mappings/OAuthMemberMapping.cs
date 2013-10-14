using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.orm.mappings
{
    internal class OAuthMemberMapping : EntityTypeConfiguration<OAuthMember>
    {
        public OAuthMemberMapping()
            : base()
        {
            this.ToTable("oauthmembers", "gdm");

            this.Property(t => t.Provider).HasColumnName("oauthmember_provider").IsRequired().HasMaxLength(30);
            this.Property(t => t.ProviderUserId).HasColumnName("oauthmember_provideruserid").IsRequired().HasMaxLength(128);
        }        
    }
}
