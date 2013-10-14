using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Helpers
{
    public static class BootstrapExtensions
    {
        /*
         * <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
          <h4 class="modal-title">Modal title</h4>
        </div>
        <div class="modal-body">
          ...
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
  </div><!-- /.modal -->
         * */
        public static MvcHtmlString BootstrapModalPopupConfirmation(this HtmlHelper htmlHelper, string socialMediaName)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("modal");
            div.AddCssClass("fade");
            div.Attributes.Add("role", "dialog");
            div.Attributes.Add("aria-labelledby", "myModalLabel");
            div.Attributes.Add("aria-hidden", "true");

            var divDialog = new TagBuilder("div");
            divDialog.AddCssClass("model-dialog");

            var divContent = new TagBuilder("div");
            divDialog.AddCssClass("modal-content");

            var divHeader = new TagBuilder("div");
            divDialog.AddCssClass("modal-header");

            var divBody = new TagBuilder("div");
            divDialog.AddCssClass("modal-body");

            var divFooter = new TagBuilder("div");
            divDialog.AddCssClass("modal-footer");

            return MvcHtmlString.Create(div.ToString());
        }
    }
}