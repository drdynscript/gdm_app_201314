using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.orm;
using Models;

namespace WebApp.Areas.Backoffice.Controllers
{
    public class ArticleController : Controller
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
        // GET: /Backoffice/Article/

        public ActionResult Index()
        {
            var model = this.UnitOfWork.ArticleRepository.Get(null, null,"Categories");
            return View(model);
        }

        //
        // GET: /Backoffice/Article/Details/5

        public ActionResult Details(int id)
        {
            var model = this.UnitOfWork.ArticleRepository.GetByID(id);
            return View(model);
        }

        //
        // GET: /Backoffice/Article/Create

        public ActionResult Create()
        {
            var model = new Article();
            return View(model);
        }

        //
        // POST: /Backoffice/Article/Create

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
        // GET: /Backoffice/Article/Edit/5

        public ActionResult Edit(int id)
        {
            var model = this.UnitOfWork.ArticleRepository.GetByID(id);
            return View(model);
        }

        //
        // POST: /Backoffice/Article/Edit/5

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
        // GET: /Backoffice/Article/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Backoffice/Article/Delete/5

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
    }
}
