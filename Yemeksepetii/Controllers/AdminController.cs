using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Yemeksepetii.Models;
using Yemeksepetii.App_Classes;

namespace Yemeksepetii.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //construction
        public ActionResult Main()
        {
            return View();
        }




        // SHOW COMPANIES // LAST MODIFIED: 2019-08-02
        public ActionResult CompanyShow()
        {
            List<MembershipUser> companyList = Roles.GetUsersInRole("Company").Select(Membership.GetUser).ToList();

            return View(companyList);
        }

        // DELETE COMPANY // LAST MODIFIED: 2019-08-02
        [HttpPost]
        public void CompanyDelete(Guid companyid)
        {
            MembershipUser mu = Membership.GetUser(companyid, false);
            Membership.DeleteUser(mu.UserName, true);

            string email = mu.Email;

            Users company = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            Context.Baglanti.Users.Remove(company);
            Context.Baglanti.SaveChanges();
        }
        



        // SHOW CUSTOMERS // LAST MODIFIED: 2019-08-02
        public ActionResult CustomerShow()
        {
            List<MembershipUser> customerList = Roles.GetUsersInRole("Customer").Select(Membership.GetUser).ToList();

            return View(customerList);
        }

        // DELETE CUSTOMER // LAST MODIFIED: 2019-08-02
        [HttpPost]
        public void CustomerDelete(Guid customerid)
        {
            MembershipUser mu = Membership.GetUser(customerid, false);
            Membership.DeleteUser(mu.UserName, true);

            string email = mu.Email;

            Users customer = Context.Baglanti.Users.FirstOrDefault(x => x.Email == email);
            Context.Baglanti.Users.Remove(customer);
            Context.Baglanti.SaveChanges();
        }




        // SHOW ROLES // LAST MODIFIED: 2019-08-01
        public ActionResult RoleShow()
        {
            List<string> roles = Roles.GetAllRoles().ToList();
            return View(roles);
        }

        // DELETE ROLE // LAST MODIFIED: 2019-08-02
        [HttpPost]
        public void RoleDelete(string RoleName)
        {
            Roles.DeleteRole(RoleName);
        }

        // ADD ROLE // LAST MODIFIED: 2019-08-01
        public ActionResult RoleAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RoleAdd(string RoleName)
        {
            Roles.CreateRole(RoleName);
            return RedirectToAction("RoleShow");
        }

        // ASSIGN ROLE TO A MEMBER // (ADMIN,COMPANY,CUSTOMER) LAST MODIFIED: 2019-08-01
        public ActionResult RoleAssign()
        {
            ViewBag.Roles = Roles.GetAllRoles().ToList();
            ViewBag.Members = Membership.GetAllUsers();
            return View();
        }
        [HttpPost]
        public ActionResult RoleAssign(string Username, string RoleName)
        {
            Roles.AddUserToRole(Username, RoleName);
            return RedirectToAction("CustomerShow");
        }
        



        // SHOW CATEGORIES //
        public ActionResult CategoryShow()
        {
            List<Categories> categories = Context.Baglanti.Categories.ToList();
            return View(categories);
        }

        // DELETE CATEGORY //
        [HttpPost]
        public void CategoryDelete(int id)
        {
            Categories category = Context.Baglanti.Categories.FirstOrDefault(x => x.CategoryID == id);
            Context.Baglanti.Categories.Remove(category);
            Context.Baglanti.SaveChanges();
        }

        // ADD CATEGORY //
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Categories Category)
        {
            Context.Baglanti.Categories.Add(Category);
            Context.Baglanti.SaveChanges();

            return RedirectToAction("CategoryShow");
        }































        // UNDER CONSTRUCTION, ADD VIEW
        public ActionResult LocationShow()
        {
            return View();
        }

        //construction, add view
        public ActionResult Locations()
        {
            return View();
        }

        //construction, add view
        public ActionResult LocationAdd()
        {
            return View();
        }

        
    }
}