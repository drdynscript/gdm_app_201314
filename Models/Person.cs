using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person
    {
        public Int32 Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        [NotMapped]
        public string FullName {
            get { return FirstName + " " + SurName; }
        }
        public string Profile { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }  
    }
}
