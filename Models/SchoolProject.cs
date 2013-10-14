using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models
{
    public class SchoolProject:Entity
    {
        [Required]
        [AllowHtml]
        public string Body { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public string AcademicYear { get; set; }

        [Required]
        public Int16 LevelOfDifficultyId { get; set; }

        public virtual LevelOfDifficulty LevelOfDifficulty { get; set; }
        public virtual ICollection<User> Mentors { get; set; }
        public virtual ICollection<SchoolProjectEntry> SchoolProjectEntries { get; set; }
    }
}
