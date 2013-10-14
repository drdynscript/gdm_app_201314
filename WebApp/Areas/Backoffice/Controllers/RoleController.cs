using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.orm;
using Models;

namespace WebApp.Areas.Backoffice.Controllers
{
    public class RoleController : Controller
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
        // GET: /Backoffice/Role/

        public ActionResult Index()
        {
            var model = this.UnitOfWork.RoleRepository.Get();
            return View(model);
        }

        //
        // GET: /Backoffice/Role/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Backoffice/Role/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Backoffice/Role/Create

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
        // GET: /Backoffice/Role/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Backoffice/Role/Edit/5

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
        // GET: /Backoffice/Role/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Backoffice/Role/Delete/5

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
        public ActionResult RoleCreatePartialView()
        {
            var model = new Role();
            return PartialView("_RoleCreatePartialView", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreatePartialView(Role model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.UnitOfWork.RoleRepository.Insert(model);
                    this.UnitOfWork.Save();
                    return Redirect("Role/Index");
                }
                return PartialView("_RoleCreatePartialView", model);
            }
            catch
            {
                return PartialView("_RoleCreatePartialView", model);
            }
        }
        public ActionResult RoleListPartialView()
        {
            var model = this.UnitOfWork.RoleRepository.Get();
            return PartialView("_RoleListPartialView", model);
        }
        #endregion
    }
}
