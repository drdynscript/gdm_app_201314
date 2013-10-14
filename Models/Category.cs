using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Category
    {
        /*public Category()
        {
            this.Entities = new HashSet<Entity>();
            this.ChildCategories = new HashSet<Category>();
        }*/

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Nullable<int> ParentId { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
