using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Areas.Admin.Models
{
    public class SchoolProjectEditViewModel
    {
        public int[] SelectedCategoriesIds { get; set; }
        public global::Models.SchoolProject SchoolProject { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Categories { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> LevelsOfDifficulty { get; set; }
    }
}