using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemeksepetii.App_Classes
{
    public class SBRel
    {
        public int SellerID { get; set; }
        public int OrdererLocationID { get; set; }
        public string OrdererDistrict { get; set; }
        public double MOA { get; set; }
        public int ArrivalTimeID { get; set; }
        public string ArrivalText { get; set; }
    }
}