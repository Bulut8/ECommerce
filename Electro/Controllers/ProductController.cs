using Electro.BLL.Repositories;
using Electro.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electro.ViewModels;

namespace Electro.Controllers
{
    public class ProductController : Controller
    {
        Repository<Product> repoProduct = new Repository<Product>();
        public ActionResult Index()
        {
            return View(repoProduct.GetAll().ToList());
        }
        public ActionResult Detail(int ID)
        {
            Product product = repoProduct.GetAll().Include(a => a.Category).Include(i => i.Pictures).Where(w => w.ID == ID).FirstOrDefault();

            ViewBag.BenzerUrunler = repoProduct.GetAll().Include(a => a.Category).Include(i => i.Pictures).Where(w => w.CategoryID == product.CategoryID && w.ID != product.ID);

            return View(product);
        }
        public ActionResult Store(int? CatID)
        {
          ViewBag.Bil=repoProduct.GetAll().Include(a => a.Category).Include(i => i.Pictures).Where(w => w.CategoryID == CatID);
            return View();
        }
    



    }

}
    
