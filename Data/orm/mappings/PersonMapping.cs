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
    internal class PersonMapping : EntityTypeConfiguration<Person>
    {
        public PersonMapping()
            : base()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("person_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.FirstName).HasColumnName("person_firstname").IsRequired().HasMaxLength(45);
            this.Property(t => t.SurName).HasColumnName("person_surname").IsRequired().HasMaxLength(255);
            this.Property(t => t.Profile).HasColumnName("person_profile").IsOptional();
            this.Property(t => t.CreatedDate).HasColumnName("person_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.ModifiedDate).HasColumnName("person_modified").IsOptional();
            this.Property(t => t.DeletedDate).HasColumnName("person_deleted").IsOptional();


            //TABLE PER HIERARCHY (DISCRIMINATOR)
            this.Map<Person>(m => m.Requires("person_type").HasValue("PERSON"))
                .Map<Student>(m => m.Requires("person_type").HasValue("STUDENT"))
                .Map<Lecturer>(m => m.Requires("person_type").HasValue("LECTURER")).ToTable("persons", "gdm");
        }        
    }
}
