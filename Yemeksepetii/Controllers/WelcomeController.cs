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
    [AllowAnonymous]
    public class WelcomeController : Controller
    {

        YS_Model m = new YS_Model();
        readonly List<Cities> listCities = Context.Baglanti.Cities.ToList();
        List<Locations> listDistrict = Context.Baglanti.Locations.ToList();

        public ActionResult GetCities()
        {
            return Json(listCities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistricts(int CityID)
        {
            var districts = listDistrict.Where(d => d.CityID == CityID);
            return Json(districts, JsonRequestBehavior.AllowGet);
        }


        // CREATE ACCOUNT // LAST MODIFIED: 2019-08-01
        public ActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(Customer c)
        {
            Membership.CreateUser(c.Username, c.Password, c.Email,
                c.Question, c.Answer, true, out MembershipCreateStatus status);

            string message = "";

            switch (status)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    message += " Bu kullanıcı adı geçersiz.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    message += " Bu şifre uygun değil.";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    message += " Bu soru geçersiz.";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    message += " Bu cevap geçersiz.";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    message += " Bu e-posta geçersiz.";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    message += " Bu kullanıcı adı zaten kullanılıyor.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    message += " Bu e-posta zaten kullanılıyor.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    message += " Kullanıcı engel hatası.";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    message += " Bu kullanıcı anahtarı geçersiz.";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    message += " Bu kullanıcı anahtarı zaten kullanılıyor.";
                    break;
                case MembershipCreateStatus.ProviderError:
                    message += " Üye yönetimi sağlayıcısı hatası.";
                    break;
                default:
                    break;
            }

            ViewBag.Message = message;

            if (status == MembershipCreateStatus.Success)
            {

                Roles.AddUserToRole(c.Username, "Customer");
                
                //ADDING TO DB//
                m.Users.Add(new Users
                {
                    UserType = 3,
                    Name = c.Name,
                    Surname = c.Surname,
                    Email = c.Email
                });

                m.SaveChanges();

                ViewBag.Message = "Hesabınız başarıyla oluşturuldu!";
                return RedirectToAction("SignIn");
            }

            else
                return View();
        }



        // CREATE COMPANY //  LAST MODIFIED: 2019-08-06
        public ActionResult CreateCompany()
        {
            List<Cities> citiesList = Context.Baglanti.Cities.ToList();
            citiesList = citiesList.OrderBy(x => x.CityID).ToList();
            ViewBag.Cities = citiesList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateCompany(Company c)
        {

            Membership.CreateUser(c.Username, c.Password, c.Email,
                c.Question, c.Answer, true, out MembershipCreateStatus status);

            string message = "";

            switch (status)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    message += " Bu kullanıcı adı geçersiz.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    message += " Bu şifre uygun değil.";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    message += " Bu soru geçersiz.";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    message += " Bu cevap geçersiz.";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    message += " Bu e-posta geçersiz.";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    message += " Bu kullanıcı adı zaten kullanılıyor.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    message += " Bu e-posta zaten kullanılıyor.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    message += " Kullanıcı engel hatası.";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    message += " Bu kullanıcı anahtarı geçersiz.";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    message += " Bu kullanıcı anahtarı zaten kullanılıyor.";
                    break;
                case MembershipCreateStatus.ProviderError:
                    message += " Üye yönetimi sağlayıcısı hatası.";
                    break;
                default:
                    break;
            }

            ViewBag.Message = message;

            if (status == MembershipCreateStatus.Success)
            {

                Roles.AddUserToRole(c.Username, "Company");

                //ADDING TO DB//
                m.Users.Add(new Users
                {
                    UserType = 2,
                    Name = c.Name,
                    Surname = c.Branch,
                    Email = c.Email
                });

                m.SaveChanges();

                ViewBag.Message = "Şirket başarıyla oluşturuldu!";
                return RedirectToAction("SignIn");
            }

            else
                return View();
        }



        // SIGN IN // LAST MODIFIED: 2019-08-02
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Customer c, string remember)
        {
            bool result = Membership.ValidateUser(c.Username, c.Password);

            if (result)
            {
                if (remember == "on")
                {
                    FormsAuthentication.RedirectFromLoginPage(c.Username, true);
                }
                else
                    FormsAuthentication.RedirectFromLoginPage(c.Username, false);

                return RedirectToAction("RedirectPage", "Welcome");


            }

            else
            {
                ViewBag.Message = "Kullanıcı adı veya parola hatalı!";
                return View();
            }
        }



        // REDIRECT PAGE // LAST MODIFIED: 2019-08-02
        public ActionResult RedirectPage()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Main", "Admin");
            }

            else if (User.IsInRole("Company"))
            {
                return RedirectToAction("Main", "Company");
            }

            else if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Main", "Main");
            }

            else
            {
                ViewBag.Message = "Kullanıcı yetkilerinde bir sorun var.";
                return RedirectToAction("SignIn", "Welcome");
            }
        }



        // FORGOT PASSWORD //  LAST MODIFIED: 2019-08-01
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(Customer c)
        {
            MembershipUser mu = Membership.GetUser(c.Username);

            if (mu.PasswordQuestion == c.Question)
            {
                string pwd = mu.ResetPassword(c.Answer);
                mu.ChangePassword(pwd, c.Password);

                return RedirectToAction("SignIn");
            }

            else
            {
                ViewBag.Message = "Girdiğiniz bilgiler hatalı!";
                return View();
            }
        }



        // SIGN OUT // LAST MODIFIED: 2019-08-01
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "Welcome");
        }
    }
}