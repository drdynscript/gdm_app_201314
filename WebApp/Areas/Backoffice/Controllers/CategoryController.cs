using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.orm;
using Models;
using WebApp.Areas.Backoffice.Models;

namespace WebApp.Areas.Backoffice.Controllers
{
    public class CategoryController : Controller
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

        //
        // GET: /Backoffice/Category/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Backoffice/Category/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Backoffice/Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Backoffice/Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Backoffice/Category/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Backoffice/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Backoffice/Category/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Backoffice/Category/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region PARTIAL VIEWS
        [ChildActionOnly]
        public ActionResult CategoryCreatePartialView()
        {
            var model = new CategoryCreateViewModel();
            model.Category = new Category();
            model.Categories = this.UnitOfWork.CategoryRepository.Get();
            return PartialView("_CategoryCreatePartialView", model);
        }
        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreatePartialView(CategoryCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.UnitOfWork.CategoryRepository.Insert(model.Category);
                    this.UnitOfWork.Save();
                    return RedirectToAction("Index","Category");
                }
                return PartialView("_CategoryCreatePartialView", model);                
            }
            catch
            {
                return PartialView("_CategoryCreatePartialView", model);
            }
        }
        [ChildActionOnly]
        public ActionResult CategoryListPartialView()
        {
            var model = this.UnitOfWork.CategoryRepository.Get();
            return PartialView("_CategoryListPartialView", model);
        }
        #endregion
    }
}
