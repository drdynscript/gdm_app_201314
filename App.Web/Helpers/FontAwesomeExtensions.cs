using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace App.Web.Helpers
{
    public static class FontAwesomeExtensions
    {
        public static MvcHtmlString FontAwesomeSocialMedia(this HtmlHelper htmlHelper, string socialMediaName)
        {
            var i = new TagBuilder("i");
            i.AddCssClass("icon-large");
            switch (socialMediaName)
            {
                case "facebook":
                    i.AddCssClass("icon-facebook");
                    break;
                case "twitter":
                    i.AddCssClass("icon-twitter");
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(i.ToString());
        }
    }
}