using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Admin.Controllers
{
    public class SchoolProjectEntryController : Controller
    {
        #region DB
        private Data.orm.UnitOfWork _unitOfWork;
        public Data.orm.UnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = new Data.orm.UnitOfWork();
                return _unitOfWork;
            }
            set { _unitOfWork = value; }
        }
        #endregion

        public ActionResult Index()
        {
            var entries = this.UnitOfWork.SchoolProjectEntryRepository.Get();
            return View(entries);
        }

        public ActionResult Create()
        {
            var model = new App.Web.Areas.Admin.Models.SchoolProjectEntryEditViewModel()
            {
                SchoolProjects = new MultiSelectList(UnitOfWork.SchoolProjectRepository.Get(), "Id", "Title"),
                Participants = new MultiSelectList(UnitOfWork.UserRepository.Get(), "Id", "Person.FullName"),
                SchoolProjectEntry = new global::Models.SchoolProjectEntry()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(App.Web.Areas.Admin.Models.SchoolProjectEntryEditViewModel schoolProjectEntryViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new App.Web.Areas.Admin.Models.SchoolProjectEntryEditViewModel()
                    {
                        SchoolProjects = new MultiSelectList(UnitOfWork.SchoolProjectRepository.Get(), "Id", "Title"),
                        Participants = new MultiSelectList(UnitOfWork.UserRepository.Get(), "Id", "Person.FullName"),
                        SchoolProjectEntry = schoolProjectEntryViewModel.SchoolProjectEntry
                    };
                    return View(model);
                }

                var entry = schoolProjectEntryViewModel.SchoolProjectEntry;

                var ids = schoolProjectEntryViewModel.SelectedParticipantsIds;
                if (ids != null && ids.Length > 0)
                {
                    var users = new List<global::Models.User>();
                    foreach (var id in ids)
                    {
                        var user = UnitOfWork.UserRepository.GetByID(Convert.ToInt32(id));
                        users.Add(user);
                    }
                    entry.Participants = users;
                }

                this.UnitOfWork.SchoolProjectEntryRepository.Insert(entry);
                this.UnitOfWork.Save();

                return RedirectToAction("Index", "SchoolProjectEntry", new { area = "Admin" });
            }
            catch
            {
                var model = new App.Web.Areas.Admin.Models.SchoolProjectEntryEditViewModel()
                {
                    SchoolProjects = new MultiSelectList(UnitOfWork.SchoolProjectRepository.Get(), "Id", "Title"),
                    Participants = new MultiSelectList(UnitOfWork.UserRepository.Get(), "Id", "Person.FullName"),
                    SchoolProjectEntry = schoolProjectEntryViewModel.SchoolProjectEntry
                };
                return View(model);
            }
        }
    }
}
