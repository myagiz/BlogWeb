using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BlogWebUI.Models;
using PagedList;

namespace BlogWebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
       BlogWebUIEntities db=new BlogWebUIEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Kategoriler(int sayfa=1)
        {
            var kategorilistesi = db.tblKategori.ToList().ToPagedList(sayfa,8);
            return View(kategorilistesi);
        }

        public ActionResult YeniKategoriEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniKategoriEkle(tblKategori kategori)
        {
            db.tblKategori.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Kategoriler");
        }
        
        public ActionResult KategoriSil(int id)
        {
            var kategorisil = db.tblKategori.Find(id);
            if (id==null)
            {
                HttpNotFound();
            }

            db.tblKategori.Remove(kategorisil);
            db.SaveChanges();
            return RedirectToAction("Kategoriler");
        }

        public ActionResult KategoriDuzenle(int id)
        {
            var kategoriduzenle = db.tblKategori.Where(x => x.tblKategoriId == id).SingleOrDefault();
            if (id==null)
            {
                HttpNotFound();
            }
            return View(kategoriduzenle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriDuzenle(int id, tblKategori k)
        {
            if (ModelState.IsValid)
            {
                var kategoriduzenle = db.tblKategori.Where(x => x.tblKategoriId == id).SingleOrDefault();
                kategoriduzenle.tblKategoriBaslik = k.tblKategoriBaslik;
                kategoriduzenle.tblKategoriIcerik = k.tblKategoriIcerik;
                db.SaveChanges();
                return RedirectToAction("Kategoriler");
            }

            return View(k);
        }



        public ActionResult Bloglar(int sayfa=1)
        {
            var blog = db.tblGonderi.ToList().OrderByDescending(x =>x.gonderiId).ToPagedList(sayfa, 8);
            return View(blog);
        }

        public ActionResult YeniBlogEkle()
        {
            ViewBag.gonderiKullaniciAdminId = new SelectList(db.tblAdminRol, "adminRolId", "adminRolBaslik");
            ViewBag.gonderiEtiketId = new SelectList(db.tblEtiket, "etiketId", "etiketBaslik");
            ViewBag.gonderiKategoriId = new SelectList(db.tblKategori, "tblKategoriId", "tblKategoriBaslik");
            ViewBag.gonderiKullaniciId = new SelectList(db.tblKullanici, "kullaniciId", "kullaniciAdSoyad");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]

        public ActionResult YeniBlogEkle(tblGonderi p,HttpPostedFileBase gonderiOzet)
        {
            if (gonderiOzet!=null)
            {
                WebImage img= new WebImage(gonderiOzet.InputStream);
                FileInfo imginfo=new FileInfo(gonderiOzet.FileName);

                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(800, 500);
                img.Save("~/Uploads/Blog/"  + blogimgname);

                p.gonderiOzet= "/Uploads/Blog/" + blogimgname;

            }
            db.tblGonderi.Add(p);
            db.SaveChanges();
            return RedirectToAction("Bloglar");
        }

        public ActionResult BlogSil(int id)
        {
            var blogsil = db.tblGonderi.Find(id);
            if (id==null)
            {
                HttpNotFound();
            }
            db.tblGonderi.Remove(blogsil);
            db.SaveChanges();
            return RedirectToAction("Bloglar");
        }

        public ActionResult BlogDuzenle(int id)
        {
            var blogduzenle = db.tblGonderi.Where(x => x.gonderiId == id).SingleOrDefault();
            if (blogduzenle==null)
            {
                HttpNotFound();
            }
            ViewBag.gonderiKullaniciAdminId = new SelectList(db.tblAdminRol, "adminRolId", "adminRolBaslik");
            ViewBag.gonderiEtiketId = new SelectList(db.tblEtiket, "etiketId", "etiketBaslik");
            ViewBag.gonderiKategoriId = new SelectList(db.tblKategori, "tblKategoriId", "tblKategoriBaslik");
            ViewBag.gonderiKullaniciId = new SelectList(db.tblKullanici, "kullaniciId", "kullaniciAdSoyad");
            return View(blogduzenle);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult BlogDuzenle(int id,tblGonderi p,HttpPostedFileBase gonderiOzet)
        {
            if (ModelState.IsValid)
            {
                var blogduzenle = db.tblGonderi.Where(x => x.gonderiId == id).SingleOrDefault();
                if (gonderiOzet != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(blogduzenle.gonderiOzet)))
                    {
                        System.IO.File.Delete(Server.MapPath(blogduzenle.gonderiOzet));
                    }
                    WebImage img = new WebImage(gonderiOzet.InputStream);
                    FileInfo imginfo = new FileInfo(gonderiOzet.FileName);

                    string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(800, 500);
                    img.Save("~/Uploads/Blog/" + blogimgname);

                    blogduzenle.gonderiOzet = "/Uploads/Blog/" + blogimgname;
                }
                blogduzenle.gonderiKategoriId = p.gonderiKategoriId;
                blogduzenle.gonderiBaslik = p.gonderiBaslik;
                blogduzenle.gonderiEtiketId = p.gonderiEtiketId;
                blogduzenle.gonderiYayinlanmaTarihi = p.gonderiYayinlanmaTarihi;
                blogduzenle.gonderiKullaniciId = p.gonderiKullaniciId;
                blogduzenle.gonderiKullaniciAdminId = p.gonderiKullaniciAdminId;
                blogduzenle.gonderiIcerik = p.gonderiIcerik;
                db.SaveChanges();
                return RedirectToAction("Bloglar");
            }

            return View(p);
        }

        public ActionResult Kullanicilar(int sayfa=1)
        {
            var kullanicilar=db.tblKullanici.ToList().OrderByDescending(x =>x.kullaniciId).ToPagedList(sayfa,8);
            return View(kullanicilar);
        }

        public ActionResult KullaniciEkle()
        {
            @ViewBag.kullaniciUyelikRolId=new SelectList(db.tblNormalRol,"normalRolId","normalRolBaslik");
            @ViewBag.kullaniciAdminId=new SelectList(db.tblAdminRol,"adminRolId","adminRolBaslik");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciEkle(tblKullanici p,string kullaniciSifreHash)
        {
            p.kullaniciSifreHash = Crypto.Hash(kullaniciSifreHash,"MD5");
            db.tblKullanici.Add(p);
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }

        public ActionResult KullaniciSil(int id)
        {
            var kullanicisil = db.tblKullanici.Find(id);
            if (id==null)
            {
                HttpNotFound();
            }

            db.tblKullanici.Remove(kullanicisil);
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }

        public ActionResult KullaniciDuzenle(int id)
        {
            var kullaniciduzenle = db.tblKullanici.Where(x => x.kullaniciId == id).SingleOrDefault();
            if (id==null)
            {
                HttpNotFound();
            }
            @ViewBag.kullaniciUyelikRolId = new SelectList(db.tblNormalRol, "normalRolId", "normalRolBaslik");
            @ViewBag.kullaniciAdminId = new SelectList(db.tblAdminRol, "adminRolId", "adminRolBaslik");
            return View(kullaniciduzenle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciDuzenle(int id,tblKullanici p)
        {
            if (ModelState.IsValid)
            {
                var kullaniciduzenle = db.tblKullanici.Where(x => x.kullaniciId == id).SingleOrDefault();
                kullaniciduzenle.kullaniciAdSoyad = p.kullaniciAdSoyad;
                kullaniciduzenle.kullaniciAdminId = p.kullaniciAdminId;
                kullaniciduzenle.kullaniciSifreHash = p.kullaniciSifreHash;
                kullaniciduzenle.kullaniciTelNo = p.kullaniciTelNo;
                kullaniciduzenle.kullaniciEmail = p.kullaniciEmail;
                kullaniciduzenle.kullaniciInstagram = p.kullaniciInstagram;
                kullaniciduzenle.kullaniciTwitter = p.kullaniciTwitter;
                kullaniciduzenle.kullaniciGitHub = p.kullaniciGitHub;
                kullaniciduzenle.kullaniciLinkedin = p.kullaniciLinkedin;
                kullaniciduzenle.kullaniciKayitOlmaTarihi = p.kullaniciKayitOlmaTarihi;
                db.SaveChanges();
                return RedirectToAction("Kullanicilar");
            }

            return View(p);
        }

        public ActionResult Yetkiler()
        {
            return View();
        }

        public ActionResult YetkiEkle(tblAdminRol p)
        {
            return View();
        }

       
    }
}