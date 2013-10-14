using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.orm.mappings
{
    internal class PersonalProjectMapping : EntityTypeConfiguration<PersonalProject>
    {
        public PersonalProjectMapping()
            : base()
        {
            this.ToTable("personalprojects", "gdm");

            this.Property(t => t.Body).HasColumnName("personalproject_body").IsOptional();
        }        
    }
}
