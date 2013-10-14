using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.orm;

namespace App.Web.Areas.Admin.Controllers
{
    public class MediaController : Controller
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

        public ActionResult Index()//All MEDIA
        {
            return View();
        }

        public ActionResult Upload()//ALL MEDIA TO UPLOAD
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Server.MapPath("~/uploads");
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fullPathFile = Path.Combine(path, newFileName);
                file.SaveAs(fullPathFile);
                var fileUIId = this.Request.Form["fileUIId"];

                return PartialView("_MediumCreatePartial", new global::Models.Medium { Url = "uploads/" + newFileName, MimeType = file.ContentType, MediumType = Data.utilities.MediumTypes.MediumTypeFromMimeType(file.ContentType)});
            }
            else
            {
                Response.StatusCode = 400;
                return Json(new { errormessage = "Geen file geselecteerd" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreateMedium(global::Models.Medium model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.UnitOfWork.MediumRepository.Insert(model);
                    this.UnitOfWork.Save();

                    return MediumListPartialView();
                }
                return PartialView("_MediumCreatePartial", model);
            }
            catch
            {
                return PartialView("_MediumCreatePartial", model);
            }
        }

        [HttpPost]
        public ActionResult AjaxDeleteFile(string fileUrl)
        {
            try
            {
                if (fileUrl != null && fileUrl.Length > 0 && Server.MapPath("~/" + fileUrl) != null)
                {
                    var path = Server.MapPath("~/" + fileUrl);
                    System.IO.File.Delete(path);

                    return Json(new { message = "File deleted!" });
                }
                return Json(new { errormessage = "File does not exits!" });
            }
            catch(Exception ex)
            {
                return Json(new { errormessage = ex.Message });
            }
        }

        public ActionResult MediumListPartialView()
        {
            var model = this.UnitOfWork.MediumRepository.Get().OrderByDescending(c => c.CreatedDate).ToList();
            return PartialView("_MediumListPartial", model);
        }
    }
}
