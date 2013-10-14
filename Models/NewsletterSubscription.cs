using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NewsletterSubscription
    {
        public Int64 Id { get; set; }
        public string Email { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<Int32> UserId { get; set; }

        public virtual User User { get; set; }
    }
}
