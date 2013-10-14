using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models
{
    public class SchoolProjectEntry : Entity
    {
        [Required]
        public Int64 SchoolProjectId { get; set; }
        [AllowHtml]
        public string Body { get; set; }

        public virtual SchoolProject SchoolProject { get; set; }
        public virtual ICollection<User> Participants { get; set; }
    }
}
