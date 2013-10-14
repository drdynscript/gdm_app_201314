using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.orm.mappings
{
    internal class SchoolProjectEntryMapping : EntityTypeConfiguration<SchoolProjectEntry>
    {
        public SchoolProjectEntryMapping()
            : base()
        {
            this.ToTable("schoolprojectentries", "gdm");

            this.Property(t => t.SchoolProjectId).HasColumnName("schoolproject_id").IsRequired();
            this.Property(t => t.Body).HasColumnName("schoolprojectentry_body").IsOptional();

            //FOREIGN KEYS
            this.HasRequired(p => p.SchoolProject).WithMany(t => t.SchoolProjectEntries).HasForeignKey(f => f.SchoolProjectId);

            //MANYTOMANY RELATIONSHIPS
            this.HasMany(p => p.Participants)
                .WithMany(t => t.SchoolProjectEntriesAsAParticipant)
                .Map(mc =>
                {
                    mc.ToTable("schoolprojectentries_has_participants");
                    mc.MapLeftKey("schoolprojectentry_id");
                    mc.MapRightKey("participant_id");
                });
        }        
    }
}
