using Electro.ViewModels;
using Electro.BLL.Repositories;
using Electro.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Electro.Helpers;
using Newtonsoft.Json;

namespace Electro.Controllers
{
    public class HomeController : Controller
    {
        Repository<Admin> repoAdmin = new Repository<Admin>();
        Repository<Category> repoCategory = new Repository<Category>();
        Repository<Product> repoProduct = new Repository<Product>();
        Repository<Picture> repoPicture = new Repository<Picture>();
        Repository<Brand> repoBrand = new Repository<Brand>();

        //public ActionResult Index()
        //{
        //    IndexVM IndexVM = new IndexVM
        //    {
        //        NewestProducts = repoProduct.GetAll().Include(i => i.Pictures).Include(a=> a.Category).OrderByDescending(o => o.ID).ToList(),
        //        BestSellerProducts = repoProduct.GetAll().Include(i => i.Pictures).Include(a => a.Category).OrderByDescending(o => o.OrderDetail.Sum(s => s.Quantity)).ToList()
        //    };

        //    return View(IndexVM);
        //}
        public ActionResult Index(int? CatID, int? BrandID, string Price)
        {
            //List<Product> products = repoProduct.GetAll().Include(i => i.Pictures).ToList();

            CategoryVM categoryVM = new CategoryVM
            {
                Products = repoProduct.GetAll().Include(a => a.Pictures).Include(a => a.Category).ToList(),
                Categories = repoCategory.GetAll().Include(a => a.Products).ToList(),
                Brands = repoBrand.GetAll().Include(a => a.Products).ToList()

            };



            if (CatID.HasValue)
            {
                categoryVM.Products = repoProduct.GetAll().Include(a => a.Pictures).Include(a => a.Category).Where(a => a.Category.ID == CatID).ToList();
            }

            if (BrandID.HasValue)
            {
                categoryVM.Products = repoProduct.GetAll().Include(a => a.Pictures).Include(a => a.Category).Where(w => w.BrandID == BrandID).ToList();
            }


            if (!string.IsNullOrEmpty(Price))
            {
                string[] prices = Price.Replace("₺", "").Replace(" ", "").Split('_');
                decimal price1 = Convert.ToDecimal(prices[0]);
                decimal price2 = Convert.ToDecimal(prices[1]);
                categoryVM.Products = repoProduct.GetAll().Include(a => a.Pictures).Include(a => a.Category).Where(w => w.Price >= price1 && w.Price <= price2).ToList();
            }
           
            return View(categoryVM);
        }

        //[Authorize(Roles = "admin")]
        public ActionResult Login(string ReturnUrl)
        {
            if (!String.IsNullOrEmpty(ReturnUrl))
            {

                if (ReturnUrl.IndexOf("/Member") != -1) return Redirect("/Member/Index");
                else
                {
                    if (User.Identity.IsAuthenticated) FormsAuthentication.SignOut();
                    ViewBag.rtn = ReturnUrl;
                    return View();
                }
            }
            else return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(string kullaniciAdi, string pass, string rURL)
        {
            if (!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(pass))
            {
                Admin admin = repoAdmin.GetBy(a => a.KullaniciAdi == kullaniciAdi && a.Sifre == pass);
                if (admin != null)
                {
                    FormsAuthentication.SetAuthCookie("#" + kullaniciAdi, true);
                    Session["AdSoyad"] = admin.AdSoyad;
                    if (!string.IsNullOrEmpty(rURL)) return Redirect(rURL);
                    else return Redirect("/admin");
                }
                else
                {
                    ViewBag.Hata = "Kullanıcı Adı veya Şifre Hatalı";
                }
            }
            else ViewBag.Hata = "Kullanıcı Adı ve Şifre Gerekli";
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Header()
        {
            return PartialView(repoCategory.GetAll().Include(i => i.Children));
        }
        [HttpPost]
        public void AddCart(int productID, int quantity)
        {
            CartHelper.AddCart(productID, quantity);
        }
        [HttpPost]
        public void UpdateCart(int productID, int quantity)
        {
            CartHelper.UpdateCart(productID, quantity);
        }
        [HttpPost]
        public void DeleteCart(int productID)
        {
            CartHelper.DeleteCart(productID);
        }
        public ActionResult DilDegistir(string dil, string returnURL)
        {
            HttpCookie httpCookie = new HttpCookie("CKDil");
            httpCookie.Value = dil;
            Response.Cookies.Add(httpCookie);
            return Redirect(returnURL);
        }
       
    }
}