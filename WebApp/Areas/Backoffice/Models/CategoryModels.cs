using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace WebApp.Areas.Backoffice.Models
{
    public class CategoryCreateViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}