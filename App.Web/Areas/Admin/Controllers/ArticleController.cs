using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Admin.Controllers
{
    public class ArticleController : AdminController
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
            var articles = this.UnitOfWork.ArticleRepository.Get();
            return View(articles);
        }

        public ActionResult Create()
        {
            var model = new App.Web.Areas.Admin.Models.ArticleEditViewModel()
            {
                Categories = new MultiSelectList(UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                Article = new global::Models.Article()
            };
            return View(model);
        }

        //
        // POST: /Admin/Article/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(App.Web.Areas.Admin.Models.ArticleEditViewModel articleViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(articleViewModel);

                var article = articleViewModel.Article;
                var ids = articleViewModel.SelectedCategoriesIds;
                if (ids != null && ids.Length > 0)
                {
                    var categories = new List<global::Models.Category>();
                    foreach (var id in ids)
                    {
                        var category = UnitOfWork.CategoryRepository.GetByID(Convert.ToInt16(id));
                        categories.Add(category);
                    }
                    article.Categories = categories;
                }
                this.UnitOfWork.ArticleRepository.Insert(article);
                this.UnitOfWork.Save();

                return RedirectToAction("Index", "Article", new { area = "Admin" });
            }
            catch
            {
                return View(articleViewModel);
            }
        }

        public ActionResult Edit(Int32 id)
        {
            var article = this.UnitOfWork.ArticleRepository.GetByID(id);
            if(article == null)
                return RedirectToAction("Index", "Article", new { area = "Admin" });

            int[] ids = null;
            if (article.Categories != null && article.Categories.Count > 0)
            {
                ids = new int[article.Categories.Count];
                int i = 0;
                foreach (var category in article.Categories)
                {
                    ids[i] = category.Id;
                    i++;
                }
            }
            var model = new App.Web.Areas.Admin.Models.ArticleEditViewModel()
            {
                SelectedCategoriesIds = ids,
                Categories = new MultiSelectList(this.UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                Article = article
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(App.Web.Areas.Admin.Models.ArticleEditViewModel articleViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(new App.Web.Areas.Admin.Models.ArticleEditViewModel()
                    {
                        SelectedCategoriesIds = articleViewModel.SelectedCategoriesIds,
                        Categories = new MultiSelectList(this.UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                        Article = articleViewModel.Article
                    });

                //GET ORGINAL MODEL
                var modelOrginal = this.UnitOfWork.ArticleRepository.GetByID(articleViewModel.Article.Id);
                //ADD NEW VALUES
                modelOrginal.ModifiedDate = DateTime.UtcNow;
                modelOrginal.Title = articleViewModel.Article.Title;
                modelOrginal.Description = articleViewModel.Article.Description;
                modelOrginal.Body = articleViewModel.Article.Body;
                var ids = articleViewModel.SelectedCategoriesIds;
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
                this.UnitOfWork.ArticleRepository.Update(modelOrginal);
                this.UnitOfWork.Save();

                return RedirectToAction("Index", "Article", new { area = "Admin" });
            }
            catch
            {
                return View(new App.Web.Areas.Admin.Models.ArticleEditViewModel()
                {
                    SelectedCategoriesIds = articleViewModel.SelectedCategoriesIds,
                    Categories = new MultiSelectList(this.UnitOfWork.CategoryRepository.Get(), "Id", "Name"),
                    Article = articleViewModel.Article
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete(Int32 id)
        {
            string message = "";
            bool state = false;
            try
            {
                var article = this.UnitOfWork.ArticleRepository.GetByID(id);
                if (article != null)
                {
                    this.UnitOfWork.ArticleRepository.Delete(article);
                    this.UnitOfWork.Save();

                    message = string.Format("Deleted Id '{0}' from the database!", id);
                    state = true;
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { id = id, state = state, message = message }); 
        }

        #region Helpers
        #endregion
    }
}
