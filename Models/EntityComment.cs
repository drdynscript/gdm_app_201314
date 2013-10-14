using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EntityComment
    {
        /*public EntityComment()
        {
            this.ChildComments = new HashSet<EntityComment>();
        }*/

        public Int64 Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Nullable<DateTime> LockedDate { get; set; }
        public Nullable<Int64> ParentId { get; set; }
        public Int32 UserId { get; set; }
        public Int64 EntityId { get; set; }

        public virtual EntityComment ParentComment { get; set; }
        public virtual User User { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual ICollection<EntityComment> ChildComments { get; set; }
    }
}
