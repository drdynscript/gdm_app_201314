using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.orm.mappings
{
    internal class MediumMapping : EntityTypeConfiguration<Medium>
    {
        public MediumMapping()
            : base()
        {
            this.ToTable("media", "gdm");

            this.Property(t => t.Url).HasColumnName("medium_url").IsRequired().HasMaxLength(255);
            this.Property(t => t.IsExternal).HasColumnName("medium_isexternal").IsRequired();
            this.Property(t => t.MimeType).HasColumnName("medium_mimetype").IsRequired().HasMaxLength(255);
            this.Property(t => t.MediumType).HasColumnName("medium_type").IsRequired();

            //FOREIGN KEYS
        }        
    }
}
