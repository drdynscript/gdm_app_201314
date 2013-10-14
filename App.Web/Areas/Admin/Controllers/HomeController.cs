using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
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

        public ActionResult AmountOfUsersPartialView()
        {
            var users = this.UnitOfWork.PersonRepository.Get();
            var model = new App.Web.Areas.Admin.Models.AmountOfUsersModel
            {
                UsersCount = users.Count(),
                StudentsCount = users.OfType<global::Models.Student>().Count(),
                LecturersCount = users.OfType<global::Models.Lecturer>().Count()
            };
            return PartialView("_AmountOfUsersPartial", model);
        }

        public ActionResult ArticlesInNumbersPartialView()
        {
            var articles = this.UnitOfWork.ArticleRepository.Get();
            var model = new App.Web.Areas.Admin.Models.ArticlesInNumbersModel
            {
                ArticlesCount = articles.Count(),
                CommentsOnArticlesCount = 0,
                AuthorsArticlesCount = 0
            };
            return PartialView("_ArticlesInNumbersPartial", model);
        }
    }
}
