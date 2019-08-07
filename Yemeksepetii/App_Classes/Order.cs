using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemeksepetii.App_Classes
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderTime { get; set; }
        public int OrdererID { get; set; }
        public string OrderStatus { get; set; }
        public double Price { get; set; }
        public string Comment { get; set; }
        public int CommentAnswered { get; set; }
    }
}