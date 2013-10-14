using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
            this.Visits = new HashSet<EntityVisit>();
            this.Comments = new HashSet<EntityComment>();
            this.Entities = new HashSet<Entity>();
            this.Likes = new HashSet<Entity>();
            this.Friends = new HashSet<User>();
            this.Users = new HashSet<User>();
        }

        public Int32 Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Nullable<DateTime> LockedDate { get; set; }
        public Nullable<DateTime> LastLoggedinDate { get; set; }
        public Int32 PersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<EntityComment> Comments { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }//LINKS TO SCHOOLPROJECTS, SCHOOLPROJECTENTRIES, PERSONALPROJECTS, ARTICLES, ALBUMS, ...
        public virtual ICollection<Entity> Likes { get; set; }
        public virtual ICollection<User> Friends { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<EntityVisit> Visits { get; set; }
        public virtual ICollection<RouteVisit> Routes { get; set; }
        public virtual ICollection<Message> SendedMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<SchoolProject> SchoolProjectsAsAMentor { get; set; }
        public virtual ICollection<SchoolProjectEntry> SchoolProjectEntriesAsAParticipant { get; set; }
    }
}
