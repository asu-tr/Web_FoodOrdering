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
        // ANA SAYFA //
        public ActionResult Main()
        {
            string email = Membership.GetUser().Email;
            Users company = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            string Company = company.Name + ", " + company.Surname;
            ViewBag.CompanyName = Company;

            int CompanyID = company.UserID;
            List<Orders> orders = Context.Baglanti.Orders.ToList();
            List<OrderStats> orderStats = Context.Baglanti.OrderStats.ToList();
            List<OrderInfo> orderInfos = Context.Baglanti.OrderInfo.ToList();
            List<Comments> comments = Context.Baglanti.Comments.ToList();

            var companyOrders =
                from order in orders
                from stat in orderStats
                from info in orderInfos
                from comment in comments
                where order.SellerID == CompanyID
                && order.OrderStatusID == stat.StatID
                && order.OrderID == info.OrderID
                && order.OrderID == comment.OrderID
                select new Order
                {
                    OrderID = order.OrderID,
                    OrderTime = order.OrderTime,
                    OrdererID = order.OrdererID,
                    OrderStatus = stat.StatText,
                    Price = info.Price,
                    Comment = comment.Comment,
                    CommentAnswered = comment.Answered
                };

            var CompanyOrders = companyOrders.ToList();
            IList<Order> companyOrdersList = new List<Order>();

            int waitingOrderCount = 0;

            double totalProfit = 0;

            int commentCount = 0;
            int unansweredCommentCount = 0;

            foreach (var order in CompanyOrders)
            {
                if (order.OrderStatus == "Hazırlanıyor" || order.OrderStatus == "Yolda")
                {
                    waitingOrderCount = waitingOrderCount + 1;
                }

                if (order.OrderStatus == "Tamamlandı")
                {
                    totalProfit = totalProfit + order.Price;
                }

                if (order.Comment != "")
                {
                    commentCount = commentCount + 1;

                    if (order.CommentAnswered == 0)
                    {
                        unansweredCommentCount = unansweredCommentCount + 1;
                    }
                }

                companyOrdersList.Add(new Order()
                {
                    OrderID = order.OrderID,
                    OrderTime = order.OrderTime,
                    OrdererID = order.OrdererID,
                    OrderStatus = order.OrderStatus
                });
            }

            ViewBag.WaitingOrderCount = waitingOrderCount;

            companyOrdersList = companyOrdersList.OrderBy(x => x.OrderID).ToList();
            ViewBag.Orders = companyOrdersList;

            int orderCount = companyOrdersList.Count;
            ViewBag.OrderCount = orderCount;

            ViewBag.TotalProfit = totalProfit;
            ViewBag.UnansweredCommentCount = unansweredCommentCount;
            ViewBag.CommentCount = commentCount;
            

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















        //undercons
        public ActionResult ProfileEdit()
        {
            MembershipUser mu = Membership.GetUser();
            Users company = Context.Baglanti.Users.FirstOrDefault(x => x.Email == mu.Email);

            ViewBag.CompanyName = company.Name;
            ViewBag.CompanyBranch = company.Surname;
            ViewBag.City = company.City;
            ViewBag.District = company.District;
            ViewBag.Telephone = company.Tel;

            return View();
        }
    }
}