using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electro.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        ////[AllowAnonymous] Herkes Erısebılır
    }
}