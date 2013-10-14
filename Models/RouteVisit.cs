using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RouteVisit
    {
        public Guid Id { get; set; }
        public string SessionId { get; set; }
        public string UserAgent { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<Int32> UserId { get; set; }

        public virtual User User { get; set; }
    }
}
