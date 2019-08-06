using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yemeksepetii.Models;

namespace Yemeksepetii.App_Classes
{
    public class Context
    {
        private static YS_Model baglanti;

        public static YS_Model Baglanti
        {
            get
            {
                if (baglanti == null)
                    baglanti = new YS_Model();
                return baglanti;
            }
            set { baglanti = value; }
        }

    }
}