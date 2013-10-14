using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.orm.mappings
{
    internal class SchoolProjectMapping : EntityTypeConfiguration<SchoolProject>
    {
        public SchoolProjectMapping()
            : base()
        {
            this.ToTable("schoolprojects", "gdm");

            this.Property(t => t.Body).HasColumnName("schoolproject_body").IsOptional();
            this.Property(t => t.StartDate).HasColumnName("schoolproject_started").IsRequired();
            this.Property(t => t.EndDate).HasColumnName("schoolproject_ended").IsRequired();
            this.Property(t => t.AcademicYear).HasColumnName("schoolproject_academicyear").IsOptional();
            this.Property(t => t.LevelOfDifficultyId).HasColumnName("level_id").IsRequired();

            //FOREIGN KEYS
            this.HasRequired(p => p.LevelOfDifficulty).WithMany().HasForeignKey(f => f.LevelOfDifficultyId);

            //MANYTOMANY RELATIONSHIPS
            this.HasMany(p => p.Mentors)
                .WithMany(t => t.SchoolProjectsAsAMentor)
                .Map(mc =>
                {
                    mc.ToTable("schoolprojects_has_mentors");
                    mc.MapLeftKey("schoolproject_id");
                    mc.MapRightKey("mentor_id");
                });
        }        
    }
}
