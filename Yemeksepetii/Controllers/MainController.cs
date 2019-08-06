using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yemeksepetii.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        //edit
        public ActionResult Main()
        {
            return View();
        }
    }
}