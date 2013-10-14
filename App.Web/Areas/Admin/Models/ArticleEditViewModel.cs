using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Areas.Admin.Models
{
    public class ArticleEditViewModel
    {
        public int[] SelectedCategoriesIds { get; set; }
        public global::Models.Article Article { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Categories { get; set; }
    }
}