using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.orm.mappings;
using Models;

namespace Data.orm
{
    public class GDMContext:DbContext
    {
        public GDMContext():base("gdmCS"){}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new PersonMapping());
            modelBuilder.Configurations.Add(new MemberMapping());
            modelBuilder.Configurations.Add(new OAuthMemberMapping());

            modelBuilder.Configurations.Add(new RoleMapping());

            modelBuilder.Configurations.Add(new EntityMapping());
                                    
            modelBuilder.Configurations.Add(new ArticleMapping());
            modelBuilder.Configurations.Add(new MediumMapping());
            modelBuilder.Configurations.Add(new AlbumMapping());
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new EntityCommentMapping());
            modelBuilder.Configurations.Add(new EntityVisitMapping());
            modelBuilder.Configurations.Add(new MessageMapping());

            modelBuilder.Configurations.Add(new InfoContactMapping());
            modelBuilder.Configurations.Add(new NewsletterSubscriptionMapping());

            modelBuilder.Configurations.Add(new RouteVisitMapping());
           
            modelBuilder.Configurations.Add(new SchoolProjectMapping());
            modelBuilder.Configurations.Add(new SchoolProjectEntryMapping());
            modelBuilder.Configurations.Add(new PersonalProjectMapping());
            modelBuilder.Configurations.Add(new LevelOfDifficultyMapping()); 
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw;  // You can also choose to handle the exception here...
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException != null)
                    Trace.TraceInformation(dbEx.InnerException.Message);
                foreach (var entry in dbEx.Entries)
                {
                    foreach (var validationError in entry.GetValidationResult().ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw;  // You can also choose to handle the exception here...
            }
        }
    }
}
