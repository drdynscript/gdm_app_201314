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
    internal class RouteVisitMapping : EntityTypeConfiguration<RouteVisit>
    {
        public RouteVisitMapping()
            : base()
        {
            this.ToTable("routevisits", "gdm");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("routevisit_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.Controller).HasColumnName("routevisit_controller").IsRequired();
            this.Property(t => t.Action).HasColumnName("routevisit_action").IsRequired();
            this.Property(t => t.SessionId).HasColumnName("routevisit_sessionid").IsRequired();
            this.Property(t => t.UserAgent).HasColumnName("routevisit_useragent").IsRequired();
            this.Property(t => t.CreatedDate).HasColumnName("routevisit_created").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.UserId).HasColumnName("user_id").IsOptional();

            //FOREIGN KEYS
            this.HasOptional(p => p.User).WithMany(t => t.Routes).HasForeignKey(f => f.UserId);
        }        
    }
}
