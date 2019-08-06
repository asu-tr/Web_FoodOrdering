using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Yemeksepetii.App_Classes;
using Yemeksepetii.Models;

namespace Yemeksepetii.Controllers
{
    [Authorize(Roles = "Company")]
    public class CompanyController : Controller
    {
        //edit
        public ActionResult Main()
        {
            return View();
        }

        




        // SHOW SERVED PRODUCTS //
        public ActionResult ProductsServedShow()
        {
            string email = Membership.GetUser().Email;
            Users user = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            int sellerID = user.UserID;

            List<ServedProducts> sp = Context.Baglanti.ServedProducts.ToList();
            List<Products> products = Context.Baglanti.Products.ToList();

            var servedProducts =
                from sprod in sp
                from prod in products
                where sprod.SellerID == sellerID
                select new ServedProduct
                {
                    ServeID = sprod.ServeID,
                    ProductName = prod.ProductName,
                    Price = sprod.Price
                };

            IList<ServedProduct> servedProdList = new List<ServedProduct>();

            var sps = servedProducts.ToList();

            foreach (var prod in sps)
            {
                servedProdList.Add(new ServedProduct()
                {
                    ServeID = prod.ServeID,
                    ProductName = prod.ProductName,
                    Price = prod.Price
                });
            }

            return View(servedProdList);
        }

        // delte served prod //
        [HttpPost]
        public void ProductsServedDelete(int spID)
        {
            ServedProducts spps = Context.Baglanti.ServedProducts.FirstOrDefault(x => x.ServeID == spID);
            Context.Baglanti.ServedProducts.Remove(spps);
            Context.Baglanti.SaveChanges();
        }

        // add served product //
        public ActionResult ProductsServedAdd()
        {
            string email = Membership.GetUser().Email;
            Users user = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            int sellerID = user.UserID;
            ViewBag.SellerID = sellerID;

            List<Products> products = Context.Baglanti.Products.ToList();
            ViewBag.Products = products;

            return View();
        }
        [HttpPost]
        public ActionResult ProductsServedAdd(ServedProducts servedProduct)
        {
            ServedProducts sp = new ServedProducts { ProductID = servedProduct.ProductID, SellerID = servedProduct.SellerID, Price = servedProduct.Price };
            Context.Baglanti.ServedProducts.Add(servedProduct);
            Context.Baglanti.SaveChanges();

            return RedirectToAction("ProductsServedShow");
        }
    }
}