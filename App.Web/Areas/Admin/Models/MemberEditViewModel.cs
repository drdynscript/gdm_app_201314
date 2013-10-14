using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Areas.Admin.Models
{
    public class MemberEditViewModel
    {
        public int[] SelectedRolesIds { get; set; }
        public global::Models.Member Member { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Roles { get; set; }
    }
}