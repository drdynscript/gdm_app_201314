using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Member:User
    {
        //public Member() : base() { }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public string PasswordSalt { get; set; }
        public Nullable<DateTime> PasswordChangedDate { get; set; }
        public Nullable<int> PasswordFailuresSinceLastSucces { get; set; }
        public Nullable<DateTime> PasswordLastFailureDate { get; set; }
        public string ConfirmationToken { get; set; }
        public Nullable<DateTime> ConfirmedDate { get; set; }
    }
}
