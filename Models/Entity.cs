using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Entity
    {
        /*public Entity()
        {
            this.Comments = new HashSet<EntityComment>();
            this.Likes = new HashSet<User>();
            this.Categories = new HashSet<Category>();
        }*/

        public Int64 Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        [Required]
        public Int32 UserId { get; set; }
        [Required]
        public Boolean CanBeVisitByGuests { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<User> Likes { get; set; }
        public virtual ICollection<EntityComment> Comments { get; set; }
        public virtual ICollection<EntityVisit> Visits { get; set; }
    }
}
