using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.orm.mappings
{
    internal class AlbumMapping : EntityTypeConfiguration<Album>
    {
        public AlbumMapping():base()
        {
            this.ToTable("albums", "gdm");

            //MANYTOMANY RELATIONSHIPS
            this.HasMany(p => p.Media)
                .WithMany(t => t.Albums)
                .Map(mc =>
                {
                    mc.ToTable("albums_has_media");
                    mc.MapLeftKey("album_id");
                    mc.MapRightKey("media_id");
                });
        }        
    }
}
