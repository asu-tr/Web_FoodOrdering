using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

            int CompanyID = company.LocationID;
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
                    Description = servedproduct.Descriptionn,
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

        // GET PRODUCTS OF SELECTED CATEGORY // LAST MODIFIED: 2019-08-08
        public ActionResult loadCategory(int categoryID)
        {
            return Json(Context.Baglanti.Products.Where(d => d.CategoryID == categoryID).Select(d => new
            {
                ProductID = d.ProductID,
                ProductName = d.ProductName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // ADD SERVED PRODUCT // LAST MODIFIED: 2019-08-08
        public ActionResult ProductsServedAdd()
        {
            string email = Membership.GetUser().Email;
            Users user = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            int sellerID = user.UserID;
            ViewBag.SellerID = sellerID;

            ViewBag.Categories = Context.Baglanti.Categories.OrderBy(c => c.CategoryName).ToList();

            List<Products> products = Context.Baglanti.Products.ToList();
            ViewBag.Products = products;

            return View();
        }
        [HttpPost]
        public ActionResult ProductsServedAdd(ServedProducts servedProduct)
        {
            ServedProducts sp = new ServedProducts { ProductID = servedProduct.ProductID, SellerID = servedProduct.SellerID, Price = servedProduct.Price, Descriptionn = servedProduct.Descriptionn };
            Context.Baglanti.ServedProducts.Add(sp);
            Context.Baglanti.SaveChanges();

            return RedirectToAction("ProductsServedShow");
        }

        // EDIT SERVED PRODUCT // LAST MODIFIED: 2019-08-07
        public ActionResult ProductsServedEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServedProducts sp = Context.Baglanti.ServedProducts.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            int pid = sp.ProductID;

            Products product = Context.Baglanti.Products.FirstOrDefault(x => x.ProductID == pid);
            string name = product.ProductName.ToString();

            ViewBag.ProductName = name;

            string email = Membership.GetUser().Email;
            Users user = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            int sellerID = user.UserID;

            ViewBag.SellerID = sellerID;
            ViewBag.ServeID = id;
            ViewBag.ProductID = pid;

            return View(sp);
        }
        [HttpPost]
        public ActionResult ProductsServedEdit(ServedProducts sp)
        {
            int id = sp.ServeID;
            ServedProducts entityItem = Context.Baglanti.ServedProducts.FirstOrDefault(x => x.ServeID == id);
            if (entityItem != null)
            {
                entityItem.Price = sp.Price;
                entityItem.Descriptionn = sp.Descriptionn;

                Context.Baglanti.Entry(entityItem).State = EntityState.Modified;
                Context.Baglanti.SaveChanges();
                return RedirectToAction("ProductsServedShow");
            }
            return View(sp);
        }





        // SHOW SERVED LOCATION & ORDER TIME // LAST MODIFIED: 2019-08-09
        public ActionResult SellerBuyerRelShow()
        {
            string email = Membership.GetUser().Email;
            Users user = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            int sellerID = user.UserID;

            ViewBag.SellerID = sellerID;

            List<ArrivalTime> arrivaltimes = Context.Baglanti.ArrivalTime.ToList();
            List<ArrivalTimes> timetexts = Context.Baglanti.ArrivalTimes.ToList();
            List<MinOrderAmounts> moas = Context.Baglanti.MinOrderAmounts.ToList();
            List<Locations> districts = Context.Baglanti.Locations.ToList();


            var rels =
                from arrivaltime in arrivaltimes
                from moa in moas
                from dist in districts
                from tt in timetexts
                where arrivaltime.SellerID == sellerID
                && moa.SellerID == arrivaltime.SellerID
                && moa.OrdererLocationID == arrivaltime.OrdererLocationID
                && dist.LocationID == arrivaltime.OrdererLocationID
                && tt.ArrivalTimesID == arrivaltime.ArrivalTimeID
                select new SBRel
                {
                    SellerID = arrivaltime.SellerID,
                    OrdererLocationID = arrivaltime.OrdererLocationID,
                    OrdererDistrict = dist.District,
                    MOA = moa.MOA,
                    ArrivalTimeID = arrivaltime.ArrivalTimeID,
                    ArrivalText = tt.ArrivalTimeText
                };

            var Rels = rels.ToList();

            IList<SBRel> RelList = new List<SBRel>();

            foreach (var rel in Rels)
            {
                RelList.Add(new SBRel()
                {
                    SellerID = rel.SellerID,
                    OrdererLocationID = rel.OrdererLocationID,
                    OrdererDistrict = rel.OrdererDistrict,
                    MOA = rel.MOA,
                    ArrivalTimeID = rel.ArrivalTimeID,
                    ArrivalText = rel.ArrivalText
                });
            }

            return View(RelList);
        }

        // DELETE SERVED LOCATION & ORDER TIME // LAST MODIFIED: 2019-08-09
        [HttpPost]
        public void SellerBuyerRelDelete(int SellerID, int OLID)
        {
            ArrivalTime arrivalTime = Context.Baglanti.ArrivalTime.FirstOrDefault(x => x.SellerID == SellerID && x.OrdererLocationID == OLID);
            MinOrderAmounts MOA = Context.Baglanti.MinOrderAmounts.FirstOrDefault(x => x.SellerID == SellerID && x.OrdererLocationID == OLID);

            Context.Baglanti.ArrivalTime.Remove(arrivalTime);
            Context.Baglanti.MinOrderAmounts.Remove(MOA);

            Context.Baglanti.SaveChanges();
        }

        // ADD SERVED LOCATION & ORDER TIME // LAST MODIFIED: 2019-08-09
        public ActionResult SellerBuyerRelAdd()
        {
            string email = Membership.GetUser().Email;
            Users user = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            int sellerID = user.UserID;
            ViewBag.SellerID = sellerID;

            int sellerLocID = user.LocationID;
            int cityID = Context.Baglanti.Locations.Where(l => l.LocationID == sellerLocID).Select(l => l.CityID).FirstOrDefault();
            ViewBag.Districts = Context.Baglanti.Locations.Where(l => l.CityID == cityID).OrderBy(l => l.District).ToList();

            ViewBag.ArrivalTimes = Context.Baglanti.ArrivalTimes.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult SellerBuyerRelAdd(SBRel rel)
        {
            ArrivalTime atime = new ArrivalTime { SellerID = rel.SellerID, OrdererLocationID = rel.OrdererLocationID, ArrivalTimeID = rel.ArrivalTimeID };
            Context.Baglanti.ArrivalTime.Add(atime);

            MinOrderAmounts moa = new MinOrderAmounts { SellerID = rel.SellerID, OrdererLocationID = rel.OrdererLocationID, MOA = rel.MOA };
            Context.Baglanti.MinOrderAmounts.Add(moa);

            Context.Baglanti.SaveChanges();

            return RedirectToAction("SellerBuyerRelShow");
        }

        // EDIT SERVED LOCATION & ORDER TIME // LAST MODIFIED: 2019-08-09
        public ActionResult SellerBuyerRelEdit(int? SellerID, int? OLID)
        {
            if (SellerID == null || OLID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ArrivalTime arrivalTime = Context.Baglanti.ArrivalTime.FirstOrDefault(x => x.SellerID == SellerID && x.OrdererLocationID == OLID);
            MinOrderAmounts minOrderAmounts = Context.Baglanti.MinOrderAmounts.FirstOrDefault(x => x.SellerID == SellerID && x.OrdererLocationID == OLID);

            string Dst = Context.Baglanti.Locations.Where(x => x.LocationID == OLID).Select(x => x.District).ToString();
            string Txt = Context.Baglanti.ArrivalTimes.Where(x => x.ArrivalTimesID == arrivalTime.ArrivalTimeID).Select(x => x.ArrivalTimeText).ToString();


            if (arrivalTime == null || minOrderAmounts == null)
            {
                return HttpNotFound();
            }

            SBRel sbRel = new SBRel { SellerID = arrivalTime.SellerID, OrdererLocationID = arrivalTime.OrdererLocationID, OrdererDistrict = Dst, MOA = minOrderAmounts.MOA, ArrivalTimeID = arrivalTime.ArrivalTimeID, ArrivalText = Txt };

            ViewBag.Times = Context.Baglanti.ArrivalTimes.ToList();
            ViewBag.District = sbRel.OrdererDistrict.ToString();

            return View(sbRel);
        }
        [HttpPost]
        public ActionResult SellerBuyerRelEdit(SBRel sbrel)
        {
            int SellerID = sbrel.SellerID;
            int OLID = sbrel.OrdererLocationID;

            ArrivalTime arrivalTime = Context.Baglanti.ArrivalTime.FirstOrDefault(x => x.SellerID == SellerID && x.OrdererLocationID == OLID);
            MinOrderAmounts moa = Context.Baglanti.MinOrderAmounts.FirstOrDefault(x => x.SellerID == SellerID && x.OrdererLocationID == OLID);

            if (arrivalTime != null && moa != null)
            {
                arrivalTime.ArrivalTimeID = sbrel.ArrivalTimeID;
                moa.MOA = sbrel.MOA;

                Context.Baglanti.Entry(arrivalTime).State = EntityState.Modified;
                Context.Baglanti.Entry(moa).State = EntityState.Modified;

                Context.Baglanti.SaveChanges();
                return RedirectToAction("SellerBuyerRelShow");
            }
            return RedirectToAction("SellerBuyerRelShow");
        }










        
        







        // under construction SHOW ORDERS //
        public ActionResult OrderShow()
        {
            string email = Membership.GetUser().Email;
            Users user = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            int sellerID = user.UserID;

            List<Orders> orders = Context.Baglanti.Orders.Where(o => o.SellerID == sellerID).ToList();
            ViewBag.Orders = orders;

            return View();
        }
        
        // under construction UPDATE ORDERS //
        public ActionResult OrderUpdate(int OrderID)
        {
            return View();
        }


        






        



        //undercons edit profile
        public ActionResult ProfileEdit()
        {
            MembershipUser mu = Membership.GetUser();
            Users company = Context.Baglanti.Users.FirstOrDefault(x => x.Email == mu.Email);

            ViewBag.UserID = company.LocationID;
            ViewBag.UserType = company.UserType;
            ViewBag.Email = company.Email;
            ViewBag.CompanyName = company.Name;
            ViewBag.CompanyBranch = company.Surname;
            ViewBag.Telephone = company.Tel;


            //string city = company.City.ToString();
            //List<Cities> citiesList = Context.Baglanti.Cities.ToList();
            //int index = citiesList.FindIndex(x => x.City == city);
            //citiesList.RemoveAt(index);
            //citiesList = citiesList.OrderBy(x => x.City).ToList();
            //ViewBag.Cities = citiesList;

            return View(company);
        }
        [HttpPost]
        public ActionResult ProfileEdit(Users company)
        {
            int id = company.LocationID;
            Users Company = Context.Baglanti.Users.FirstOrDefault(x => x.LocationID == id);
            if (Company != null)
            {
                //Company.City = company.City;
                //Company.District = company.District;
                Company.Tel = company.Tel;

                Context.Baglanti.Entry(Company).State = EntityState.Modified;
                Context.Baglanti.SaveChanges();
                return RedirectToAction("Main");
            }
            return View(company);
        }
        
    }
}
