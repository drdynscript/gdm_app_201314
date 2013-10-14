using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Areas.Admin.Models
{
    public class SchoolProjectEntryEditViewModel
    {
        public int[] SelectedParticipantsIds { get; set; }        
        public IEnumerable<System.Web.Mvc.SelectListItem> Participants { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> SchoolProjects { get; set; }

        public global::Models.SchoolProjectEntry SchoolProjectEntry { get; set; }
    }
}