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
    internal class LevelOfDifficultyMapping : EntityTypeConfiguration<LevelOfDifficulty>
    {
        public LevelOfDifficultyMapping()
            : base()
        {
            this.ToTable("levelsofdifficulty", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("level_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name).HasColumnName("level_name").IsRequired();
            this.Property(t => t.Description).HasColumnName("level_description").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("level_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }        
    }
}
