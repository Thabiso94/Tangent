using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tangent.Models
{
    public class User
    {
        [DisplayName("Password")]
        public string password { get; set; }

        public int id { get; set; }

        [DisplayName("First Name")]
        public string first_name { get; set; }

        [DisplayName("Last Name")]
        public string last_name { get; set; }

        [DisplayName("Username")]
        public string username { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Staff")]
        public bool is_staff { get; set; }

        [DisplayName("Super User")]
        public bool is_superuser { get; set; }
        public Profile profile { get; set; }
        public List<object> authentications { get; set; }
        public List<object> roles { get; set; }

        [DisplayName("Token")]
        public string token { get; set; }
    }

    public class Profile
    {
        public string contact_number { get; set; }
        public object status_message { get; set; }
        public object bio { get; set; }
    }
}