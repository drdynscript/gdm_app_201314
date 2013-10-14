using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EntityVisit
    {
        public Int64 Id { get; set; }
        public string SessionId { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<Int32> UserId { get; set; }
        public Int64 EntityId { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual User User { get; set; }
    }
}
