using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yemeksepetii.Models;
using Yemeksepetii.App_Classes;

namespace Yemeksepetii.Controllers
{
    [Authorize(Roles = "Admin, Company")]
    public class ACOController : Controller
    {
        // ADD PRODUCT // LAST MODIFIED: 2019-08-06
        public ActionResult ProductAdd()
        {
            ViewBag.Categories = Context.Baglanti.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Products product)
        {
            Context.Baglanti.Products.Add(product);
            Context.Baglanti.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}