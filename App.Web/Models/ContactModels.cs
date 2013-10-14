using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Web.Models
{
    public class ContactModelUser
    {
        [Required]
        public Int32 UserId { get; set; }
        [Display(Name = "Onderwerp")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Bericht")]
        public string Body { get; set; }
    }

    public class ContactModelGuest
    {
        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Onderwerp")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Bericht")]
        public string Body { get; set; }
    }

    public class NewsletterSubscriptionModelGuest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class NewsletterSubscriptionModelUser
    {
        [Required]
        public Int32 UserId { get; set; }
    }
}