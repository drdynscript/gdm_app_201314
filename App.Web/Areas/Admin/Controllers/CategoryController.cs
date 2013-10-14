using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.orm;
using Models;
using WebApp.Areas.Backoffice.Models;

namespace App.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        #region Properties
        private UnitOfWork _unitOfWork;
        public UnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = new UnitOfWork();
                return _unitOfWork;
            }
            set
            {
                _unitOfWork = value;
            }
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region PARTIAL VIEWS, AJAX ACTIONS, ...
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDeleteCategory(Int32 id)
        {
            string message = "";
            bool state = false;
            try
            {
                var model = this.UnitOfWork.CategoryRepository.GetByID(id);
                if (model != null)
                {
                    this.UnitOfWork.CategoryRepository.Delete(model);
                    this.UnitOfWork.Save();

                    message = string.Format("Deleted Category with Id '{0}' from the database!", id);
                    state = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { id = id, state = state, message = message });
        }
        public ActionResult CategoryCreatePartialView()
        {
            var model = new CategoryCreateViewModel();
            model.Category = new Category();
            model.Categories = this.UnitOfWork.CategoryRepository.Get();
            return PartialView("_CategoryCreatePartialView", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreateCategory(CategoryCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.UnitOfWork.CategoryRepository.Insert(model.Category);
                    this.UnitOfWork.Save();

                    return CategoryListPartialView();
                }
                return PartialView("_CategoryCreatePartialView", model);                
            }
            catch
            {
                return PartialView("_CategoryCreatePartialView", model);
            }
        }
        public ActionResult CategoryEditPartialView(int id)
        {
            var model = new CategoryCreateViewModel();
            model.Category = this.UnitOfWork.CategoryRepository.GetByID(id);
            model.Categories = this.UnitOfWork.CategoryRepository.Get();
            return PartialView("_CategoryEditPartialView", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxUpdateCategory(CategoryCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.UnitOfWork.CategoryRepository.Update(model.Category);
                    this.UnitOfWork.Save();

                    return CategoryListPartialView();
                }
                return PartialView("_CategoryEditPartialView", model);
            }
            catch
            {
                return PartialView("_CategoryEditPartialView", model);
            }
        }
        public ActionResult CategoryListPartialView()
        {
            var model = this.UnitOfWork.CategoryRepository.Get().OrderByDescending(c => c.CreatedDate).ToList();
            return PartialView("_CategoryListPartialView", model);
        }
        #endregion
    }
}
