using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemeksepetii.App_Classes
{
    public class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Address { get; set; }
        public int LocationID { get; set; }
    }
}