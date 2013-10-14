using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using App.Web.Models;

namespace App.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "aFWqozA9XOto44D3jrA",
                consumerSecret: "Ato3ePky3tjHEPAHtaahcW35jRdiPGTygpfWfMQks1U");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "588664574491866",
                appSecret: "6f48b80792dcb079f9aa8ce4ea572e29");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
