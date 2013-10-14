using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Message
    {
        public Int64 Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Int32 SenderId { get; set; }
        public Int32 ReceiverId { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
