using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Admin.Controllers
{
    public class SchoolProjectController : AdminController
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
            var projects = this.UnitOfWork.SchoolProjectRepository.Get();
            return View(projects);
        }

        public ActionResult Create()
        {
            var model = new App.Web.Areas.Admin.Models.SchoolProjectEditViewModel()
            {
                Categories = new MultiSelectList(UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                LevelsOfDifficulty = new MultiSelectList(UnitOfWork.LevelOfDifficultyRepository.Get(), "Id", "Name"),
                SchoolProject = new global::Models.SchoolProject()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(App.Web.Areas.Admin.Models.SchoolProjectEditViewModel schoolProjectViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new App.Web.Areas.Admin.Models.SchoolProjectEditViewModel()
                    {
                        Categories = new MultiSelectList(UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                        LevelsOfDifficulty = new MultiSelectList(UnitOfWork.LevelOfDifficultyRepository.Get(), "Id", "Name"),
                        SchoolProject = schoolProjectViewModel.SchoolProject
                    };
                    return View(model);
                }

                var project = schoolProjectViewModel.SchoolProject;

                var ids = schoolProjectViewModel.SelectedCategoriesIds;
                if (ids != null && ids.Length > 0)
                {
                    var categories = new List<global::Models.Category>();
                    foreach (var id in ids)
                    {
                        var category = UnitOfWork.CategoryRepository.GetByID(Convert.ToInt16(id));
                        categories.Add(category);
                    }
                    project.Categories = categories;
                }

                var levelId = schoolProjectViewModel.SchoolProject.LevelOfDifficultyId;
                project.LevelOfDifficulty = UnitOfWork.LevelOfDifficultyRepository.GetByID(levelId);

                this.UnitOfWork.SchoolProjectRepository.Insert(project);
                this.UnitOfWork.Save();

                return RedirectToAction("Index", "SchoolProject", new { area = "Admin" });
            }
            catch
            {
                var model = new App.Web.Areas.Admin.Models.SchoolProjectEditViewModel()
                {
                    Categories = new MultiSelectList(UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                    LevelsOfDifficulty = new MultiSelectList(UnitOfWork.LevelOfDifficultyRepository.Get(), "Id", "Name"),
                    SchoolProject = schoolProjectViewModel.SchoolProject
                };
                return View(model);
            }
        }

        public ActionResult Edit(Int64 id)
        {
            var project = this.UnitOfWork.SchoolProjectRepository.GetByID(id);
            if (project == null)
                return RedirectToAction("Index", "SchoolProject", new { area = "Admin" });

            int[] ids = null;
            if (project.Categories != null && project.Categories.Count > 0)
            {
                ids = new int[project.Categories.Count];
                int i = 0;
                foreach (var category in project.Categories)
                {
                    ids[i] = category.Id;
                    i++;
                }
            }

            var model = new App.Web.Areas.Admin.Models.SchoolProjectEditViewModel()
            {
                SelectedCategoriesIds = ids,
                Categories = new MultiSelectList(this.UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                LevelsOfDifficulty = new MultiSelectList(UnitOfWork.LevelOfDifficultyRepository.Get(), "Id", "Name"),
                SchoolProject = project
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(App.Web.Areas.Admin.Models.SchoolProjectEditViewModel schoolProjectViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(new App.Web.Areas.Admin.Models.SchoolProjectEditViewModel()
                    {
                        SelectedCategoriesIds = schoolProjectViewModel.SelectedCategoriesIds,
                        Categories = new MultiSelectList(this.UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                        LevelsOfDifficulty = new MultiSelectList(UnitOfWork.LevelOfDifficultyRepository.Get(), "Id", "Name"),
                        SchoolProject = schoolProjectViewModel.SchoolProject
                    });

                //GET ORGINAL MODEL
                var modelOrginal = this.UnitOfWork.SchoolProjectRepository.GetByID(schoolProjectViewModel.SchoolProject.Id);
                //ADD NEW VALUES
                modelOrginal.ModifiedDate = DateTime.UtcNow;
                modelOrginal.Title = schoolProjectViewModel.SchoolProject.Title;
                modelOrginal.Description = schoolProjectViewModel.SchoolProject.Description;
                modelOrginal.StartDate = schoolProjectViewModel.SchoolProject.StartDate;
                modelOrginal.EndDate = schoolProjectViewModel.SchoolProject.EndDate;
                modelOrginal.AcademicYear = schoolProjectViewModel.SchoolProject.AcademicYear;
                modelOrginal.CanBeVisitByGuests = schoolProjectViewModel.SchoolProject.CanBeVisitByGuests;
                var ids = schoolProjectViewModel.SelectedCategoriesIds;
                if (ids != null && ids.Length > 0)
                {
                    if (modelOrginal.Categories != null)
                    {
                        modelOrginal.Categories.Clear();
                    }
                    var categories = new List<global::Models.Category>();
                    foreach (var id in ids)
                    {
                        var category = this.UnitOfWork.CategoryRepository.GetByID(Convert.ToInt16(id));
                        categories.Add(category);
                    }
                    modelOrginal.Categories = categories;
                }

                var levelId = schoolProjectViewModel.SchoolProject.LevelOfDifficultyId;
                modelOrginal.LevelOfDifficulty = UnitOfWork.LevelOfDifficultyRepository.GetByID(levelId);

                this.UnitOfWork.SchoolProjectRepository.Update(modelOrginal);
                this.UnitOfWork.Save();

                return RedirectToAction("Index", "SchoolProject", new { area = "Admin" });
            }
            catch
            {
                return View(new App.Web.Areas.Admin.Models.SchoolProjectEditViewModel()
                {
                    SelectedCategoriesIds = schoolProjectViewModel.SelectedCategoriesIds,
                    Categories = new MultiSelectList(this.UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                    LevelsOfDifficulty = new MultiSelectList(UnitOfWork.LevelOfDifficultyRepository.Get(), "Id", "Name"),
                    SchoolProject = schoolProjectViewModel.SchoolProject
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete(Int64 id)
        {
            string message = "";
            bool state = false;
            try
            {
                var project = this.UnitOfWork.SchoolProjectRepository.GetByID(id);
                if (project != null)
                {
                    this.UnitOfWork.SchoolProjectRepository.Delete(project);
                    this.UnitOfWork.Save();

                    message = string.Format("Deleted Id '{0}' from the database!", id);
                    state = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { id = id, state = state, message = message });
        }

        #region Helpers
        #endregion
    }
}
