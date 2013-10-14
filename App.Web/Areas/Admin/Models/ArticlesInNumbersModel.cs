using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Areas.Admin.Models
{
    public class ArticlesInNumbersModel
    {
        public int ArticlesCount { get; set; }
        public int CommentsOnArticlesCount { get; set; }
        public int AuthorsArticlesCount { get; set; }
    }
}