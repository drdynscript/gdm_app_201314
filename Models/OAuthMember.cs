using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OAuthMember:User
    {
        //public OAuthMember() : base() { }

        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
    }
}
