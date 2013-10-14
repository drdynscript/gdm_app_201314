using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using RazorEngine;

namespace App.Web.Controllers
{
    public class AboutController : Controller
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

        public ActionResult Contact()
        {
            return View();
        }

        [App.Web.Filters.EntityVisits]
        public ActionResult Disclaimer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(App.Web.Models.ContactModelGuest model)
        {
            string retValue = "There was an error submitting the form, please try again later.";
            if (!ModelState.IsValid)
            {
                return Content(retValue);
            }
            else
            {
                using (var client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("drdynscript", "gordijn2012"),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 10000
                })
                {
                    var mail = new MailMessage();
                    mail.To.Add("drdynscript@gmail.com");
                    mail.From = new MailAddress(model.Email, model.Name);
                    mail.Subject = String.Format("Request to Contact from {0}", model.Subject);
                    mail.Body = model.Body;
                    mail.IsBodyHtml = false;

                    string physicalPath = Server.MapPath("~/Templates/Mail/ContactMailGuestHtml.cshtml");
                    string template = System.IO.File.ReadAllText(physicalPath);

                    try
                    {
                        client.Send(mail);
                        retValue = "Uw aanvraag voor Contact is met succes verstuurd. We zullen u zo snel mogelijk contacteren.";

                        //ADD INFOCONTACT TO DB
                        UnitOfWork.InfoContactRepository.Insert(new global::Models.InfoContact { Name = model.Name, Email = model.Email, Subject = model.Subject, Body = Razor.Parse(template, model), UserAgent = this.HttpContext.Request.UserAgent });                        
                        UnitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return Content(retValue);
        }

        [HttpPost]
        public ActionResult SubscribeNewsletter(App.Web.Models.NewsletterSubscriptionModelGuest model)
        {
            string retValue = "There was an error submitting the form, please try again later.";
            if (!ModelState.IsValid)
            {
                return Content(retValue);
            }
            else
            {
                try
                {
                    //CHECK IF GUEST EMAIL ALREADY SUBSCRIBED
                    var alreadySubscribed = UnitOfWork.NewsletterSubscriptionRepository.Get(n => n.Email.Equals(model.Email)).Count();
                    if (alreadySubscribed > 0)
                    {
                        retValue = "U bent reeds ingeschreven op onze maandelijkse nieuwsbrief.";
                    }
                    else
                    {
                        //ADD NEWSLETTER SUBSCRIPTION TO DB
                        UnitOfWork.NewsletterSubscriptionRepository.Insert(new global::Models.NewsletterSubscription { Email = model.Email, UserAgent = this.HttpContext.Request.UserAgent });
                        UnitOfWork.Save();

                        retValue = "U bent ingeschreven op onze maandelijkse nieuwsbrief.";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Content(retValue);
        }

        [HttpPost]
        public ActionResult SubscribeNewsletterUser(App.Web.Models.NewsletterSubscriptionModelUser model)
        {
            string retValue = "There was an error submitting the form, please try again later.";
            if (!ModelState.IsValid)
            {
                return Content(retValue);
            }
            else
            {
                try
                {
                    //CHECK IF GUEST EMAIL ALREADY SUBSCRIBED
                    var alreadySubscribed = UnitOfWork.NewsletterSubscriptionRepository.Get(n => n.UserId == model.UserId).FirstOrDefault();
                    if (alreadySubscribed != null)
                    {
                        retValue = "U bent reeds ingeschreven op onze maandelijkse nieuwsbrief.";
                    }
                    else
                    {
                        //ADD NEWSLETTER SUBSCRIPTION TO DB
                        var user = UnitOfWork.UserRepository.GetByID(model.UserId);

                        UnitOfWork.NewsletterSubscriptionRepository.Insert(new global::Models.NewsletterSubscription { User = user, UserAgent = this.HttpContext.Request.UserAgent });
                        UnitOfWork.Save();

                        retValue = "U bent ingeschreven op onze maandelijkse nieuwsbrief.";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Content(retValue);
        }
    }
}
