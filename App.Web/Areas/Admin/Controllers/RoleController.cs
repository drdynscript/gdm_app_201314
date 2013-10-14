using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.orm;
using Models;

namespace App.Web.Areas.Admin.Controllers
{
    public class RoleController : AdminController
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
        public JsonResult AjaxDeleteRole(Int16 id)
        {
            string message = "";
            bool state = false;
            try
            {
                var model = this.UnitOfWork.RoleRepository.GetByID(id);
                if (model != null)
                {
                    this.UnitOfWork.RoleRepository.Delete(model);
                    this.UnitOfWork.Save();

                    message = string.Format("Deleted Role with Id '{0}' from the database!", id);
                    state = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { id = id, state = state, message = message });
        }
        public ActionResult RoleCreatePartialView()
        {
            var model = new global::Models.Role();
            return PartialView("_RoleCreatePartialView", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreateRole(Role model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.UnitOfWork.RoleRepository.Insert(model);
                    this.UnitOfWork.Save();

                    return RoleListPartialView();
                }
                return PartialView("_RoleCreatePartialView", model);
            }
            catch
            {
                return PartialView("_RoleCreatePartialView", model);
            }
        }
        public ActionResult RoleEditPartialView(int id)
        {
            var model = this.UnitOfWork.RoleRepository.GetByID(id);
            return PartialView("_RoleEditPartialView", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxUpdateRole(global::Models.Role model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.UnitOfWork.RoleRepository.Update(model);
                    this.UnitOfWork.Save();

                    return RoleListPartialView();
                }
                return PartialView("_RoleEditPartialView", model);
            }
            catch
            {
                return PartialView("_RoleEditPartialView", model);
            }
        }
        public ActionResult RoleListPartialView()
        {
            var model = this.UnitOfWork.RoleRepository.Get().OrderByDescending(c => c.CreatedDate).ToList();
            return PartialView("_RoleListPartialView", model);
        }
        #endregion
    }
}
