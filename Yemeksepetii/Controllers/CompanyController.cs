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




        // SHOW SERVED PRODUCTS // LAST MODIFIED: 2019-08-06
        public ActionResult ProductsServedShow()
        {
            string email = Membership.GetUser().Email;
            Users user = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            int sellerID = user.UserID;

            List<ServedProducts> servedproducts = Context.Baglanti.ServedProducts.ToList();
            List<Products> products = Context.Baglanti.Products.ToList();
            List<Categories> categories = Context.Baglanti.Categories.ToList();

            var servedProducts =
                from servedproduct in servedproducts
                from product in products
                from category in categories
                where servedproduct.SellerID == sellerID
                && product.ProductID == servedproduct.ProductID
                && product.CategoryID == category.CategoryID
                select new ServedProduct
                {
                    ServeID = servedproduct.ServeID,
                    ProductName = product.ProductName,
                    Description = product.Descriptionn,
                    CategoryID = product.CategoryID,
                    CategoryName = category.CategoryName,
                    Price = servedproduct.Price
                };

            var ServedProducts = servedProducts.ToList();

            IList<ServedProduct> servedProductList = new List<ServedProduct>();

            foreach (var product in ServedProducts)
            {
                servedProductList.Add(new ServedProduct()
                {
                    ServeID = product.ServeID,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    CategoryID = product.CategoryID,
                    CategoryName = product.CategoryName,
                    Price = product.Price
                });
            }

            servedProductList = servedProductList.OrderBy(x=> x.CategoryName).ToList();

            return View(servedProductList);
        }

        // DELETE SERVED PRODUCT // LAST MODIFIED: 2019-08-05
        [HttpPost]
        public void ProductsServedDelete(int sID)
        {
            ServedProducts spps = Context.Baglanti.ServedProducts.FirstOrDefault(x => x.ServeID == sID);
            Context.Baglanti.ServedProducts.Remove(spps);
            Context.Baglanti.SaveChanges();
        }

        // ADD SERVED PRODUCT // LAST MODIFIED: 2019-08-05
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
            Context.Baglanti.ServedProducts.Add(sp);
            Context.Baglanti.SaveChanges();

            return RedirectToAction("ProductsServedShow");
        }
        
        // ADD PRODUCT // LAST MODIFIED: 2019-08-06
        public ActionResult ProductAdd()
        {
            ViewBag.Categories = Context.Baglanti.Categories.ToList();
            return View();
        }






    }
}