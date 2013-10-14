using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Web.Filters;

namespace App.Web.Controllers
{
    public class ArticleController : Controller
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
            return View();
        }

        public ActionResult Details(Int64 id)
        {
            return View();
        }

        #region PartialViews
        [EntityVisits]
        public ActionResult ArticleDetailsPartialView(Int64 id)
        {
            var model = this.UnitOfWork.ArticleRepository.GetByID(id);
            return PartialView("_ArticleDetailsPartialView", model);
        }
        public ActionResult ArticlesListPartialView(int amount = 5)
        {
            var articles = this.UnitOfWork.ArticleRepository.Get().OrderByDescending(a => a.CreatedDate).Take(amount);
            return PartialView("_ArticlesListPartialView", articles);
        }
        #endregion
    }
}
