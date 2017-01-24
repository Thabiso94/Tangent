using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tangent.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public bool is_staff { get; set; }
        public bool is_superuser { get; set; }
        public Profile profile { get; set; }
        public List<object> authentications { get; set; }
        public List<object> roles { get; set; }
    }

    public class Profile
    {
        public string contact_number { get; set; }
        public object status_message { get; set; }
        public object bio { get; set; }
    }

    public class Test
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}