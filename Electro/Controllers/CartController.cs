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
    public class CartController : Controller
    {
        Repository<Product> repoProduct = new Repository<Product>();
        public ActionResult Index()
        {
            List<Cart> carts = new List<Cart>();
            if (Request.Cookies["CKCart"] != null)
            {
                HttpCookie httpCookie = Request.Cookies["CKCart"];
                carts = JsonConvert.DeserializeObject<List<Cart>>(httpCookie.Value);
            }
            List<CartDetail> cartDetails = new List<CartDetail>();
            foreach (Cart hbc in carts)
            {
                cartDetails.Add(new CartDetail
                {
                    ProductID = hbc.ProductID,
                    Quantity = hbc.Quantity,
                    FPath = repoProduct.GetAll().Include(i => i.Pictures).Where(w => w.ID == hbc.ProductID).FirstOrDefault().Pictures.FirstOrDefault(f => f.PIndex == 1).FPath,
                    Price = repoProduct.GetBy(g => g.ID == hbc.ProductID).Price,
                    ProductName = repoProduct.GetBy(g => g.ID == hbc.ProductID).Name
                }
                );
            }
            CartVM cartVM = new CartVM
            {
                CartDetails = cartDetails,
                BestSellerProducts = repoProduct.GetAll().Include(a => a.Category).Include(i => i.Pictures).OrderByDescending(o => o.OrderDetail.Sum(s => s.Quantity)).ToList()
            };
            return View(cartVM);
        }
    }
}