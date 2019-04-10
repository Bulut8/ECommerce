using Electro.BLL.Repositories;
using Electro.BOL.Entities;
using Electro.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electro.Controllers
{
    public class FavController : Controller
    {
        Repository<Product> repoProduct = new Repository<Product>();
        public ActionResult Index()
        {
            List<Wish> wishs = new List<Wish>();
            if (Request.Cookies["CKWish"] != null)
            {
                HttpCookie httpCookie = Request.Cookies["CKWish"];
                wishs = JsonConvert.DeserializeObject<List<Wish>>(httpCookie.Value);
            }
            List<WishDetail> wishDetails = new List<WishDetail>();
            foreach (Wish hbc in wishs)
            {
                wishDetails.Add(new WishDetail
                {
                    ProductID = hbc.ProductID,
                    Quantity = hbc.Quantity,
                    FPath = repoProduct.GetAll().Include(i => i.Pictures).Where(w => w.ID == hbc.ProductID).FirstOrDefault().Pictures.FirstOrDefault(f => f.PIndex == 1).FPath,
                    Price = repoProduct.GetBy(g => g.ID == hbc.ProductID).Price,
                    ProductName = repoProduct.GetBy(g => g.ID == hbc.ProductID).Name
                }
                );
            }
            WishVM wishVM = new WishVM
            {
                WishDetails = wishDetails,
               
            };
            return View(wishVM);
        }
    }
}