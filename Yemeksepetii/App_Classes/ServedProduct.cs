using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemeksepetii.App_Classes
{
    public class ServedProduct
    {
        public int ServeID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}