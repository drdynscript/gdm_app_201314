using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ArticleController : Controller
    {
        #region DB
        private Data.orm.UnitOfWork _unitOfWork;
        public Data.orm.UnitOfWork UnitOfWork
        {
            get {
                if (_unitOfWork == null)
                    _unitOfWork = new Data.orm.UnitOfWork();
                return _unitOfWork;
            }
            set { _unitOfWork = value; }
        }
        #endregion
        //
        // GET: /Article/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var article = this.UnitOfWork.ArticleRepository.GetByID(id);
            ViewBag.Title = article.Title;
            ViewBag.Description = article.Description;
            return View();
        }

        //
        // GET: /Article/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Article/Create

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
        // GET: /Article/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Article/Edit/5

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
        // GET: /Article/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Article/Delete/5

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

        #region PartialViews aka Components
        public PartialViewResult PVListAll()
        {
            var list = UnitOfWork.ArticleRepository.Get();
            return PartialView("_ArticlesListSmallPartial", list);
        }
        public PartialViewResult PVDetails(int id)
        {
            var article = UnitOfWork.ArticleRepository.GetByID(id);            
            return PartialView("_ArticleDetailsPartial", article);
        }
        #endregion
    }
}
