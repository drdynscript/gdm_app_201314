using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Album:Entity
    {
        /*public Album():base()
        {
            this.Media = new HashSet<Medium>();
        }*/

        public virtual ICollection<Medium> Media { get; set; }
    }
}
