using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BlogWebUI.Models;

namespace BlogWebUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        BlogWebUIEntities db=new BlogWebUIEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblKullanici kullanici)
        {
            var giris = db.tblKullanici.Where(x => x.kullaniciEmail == kullanici.kullaniciEmail).SingleOrDefault();
            if (giris.kullaniciEmail == kullanici.kullaniciEmail && giris.kullaniciSifreHash == Crypto.Hash(kullanici.kullaniciSifreHash,"MD5"))
            {
                Session["kullaniciId"] = giris.kullaniciId;
                Session["kullaniciEmail"] = giris.kullaniciEmail;
                Session["kullaniciAdSoyad"] = giris.kullaniciAdSoyad;
                Session["kullaniciYetki"] = giris.kullaniciAdminId;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.HataliGiris = "Hatalı Kullanıcı Adı ve/veya Şifre Girdiniz !";
            return View(kullanici);
        }
        public ActionResult Cikis()
        {
            Session["kullaniciId"] = null;
            Session["kullaniciEMail"] = null;
            Session["kullaniciYetki"] = null;
            Session.Abandon();
            return RedirectToAction("index", "Login");
        }
    }
}