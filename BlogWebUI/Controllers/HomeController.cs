using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogWebUI.Models;
using PagedList;

namespace BlogWebUI.Controllers
{
    public class HomeController : Controller
    {
        BlogWebUIEntities db=new BlogWebUIEntities();
        [ValidateInput(false)]
        public ActionResult Index(int sayfa=1)
        {
           var gonderilistele= db.tblGonderi.ToList().OrderByDescending(x =>x.gonderiId).ToPagedList(sayfa, 6);
            return View(gonderilistele);
        }

        public ActionResult Detay(int id)
        {
            ViewBag.gonderiKullaniciAdminId = new SelectList(db.tblAdminRol, "adminRolId", "adminRolBaslik");
            ViewBag.gonderiEtiketId = new SelectList(db.tblEtiket, "etiketId", "etiketBaslik");
            ViewBag.gonderiKategoriId = new SelectList(db.tblKategori, "tblKategoriId", "tblKategoriBaslik");
            ViewBag.gonderiKullaniciId = new SelectList(db.tblKullanici, "kullaniciId", "kullaniciAdSoyad");
           
            var detay = db.tblGonderi.Include("tblKategori").Where(x => x.gonderiId == id).SingleOrDefault();
            
            return View(detay);
        }

//        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogid)
//        {
//            if (icerik == null)
//            {
//                return Json(true, JsonRequestBehavior.AllowGet);
//            }
//            db.tblYorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogId = blogid, Onay = false });
//            db.SaveChanges();

//            return Json(false, JsonRequestBehavior.AllowGet);
//        }
//}