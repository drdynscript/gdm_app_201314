using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Medium:Entity
    {
        /*public Medium():base()
        {
            this.Albums = new HashSet<Album>();
        }*/

        public string Url { get; set; }
        public bool IsExternal { get; set; }
        public string MediumType { get; set; }
        public string MimeType { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
