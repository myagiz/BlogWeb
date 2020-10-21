using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogWebUI.Models;

namespace BlogWebUI.Controllers
{
    public class TestController : Controller
    {
        private BlogWebUIEntities db = new BlogWebUIEntities();

        // GET: Test
        public ActionResult Index()
        {
            var tblGonderi = db.tblGonderi.Include(t => t.tblAdminRol).Include(t => t.tblEtiket).Include(t => t.tblKategori).Include(t => t.tblKullanici).Include(t => t.tblNormalRol);
            return View(tblGonderi.ToList());
        }

        // GET: Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGonderi tblGonderi = db.tblGonderi.Find(id);
            if (tblGonderi == null)
            {
                return HttpNotFound();
            }
            return View(tblGonderi);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            ViewBag.gonderiKullaniciAdminId = new SelectList(db.tblAdminRol, "adminRolId", "adminRolBaslik");
            ViewBag.gonderiEtiketId = new SelectList(db.tblEtiket, "etiketId", "etiketBaslik");
            ViewBag.gonderiKategoriId = new SelectList(db.tblKategori, "tblKategoriId", "tblKategoriBaslik");
            ViewBag.gonderiKullaniciId = new SelectList(db.tblKullanici, "kullaniciId", "kullaniciAdSoyad");
            ViewBag.gonderiKullaniciUyelikRolId = new SelectList(db.tblNormalRol, "normalRolId", "normalRolBaslik");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "gonderiId,gonderiKullaniciId,gonderiKullaniciUyelikRolId,gonderiKullaniciAdminId,gonderiKategoriId,gonderiBaslik,gonderiEtiketId,gonderiOzet,gonderiYayinlanma,gonderiOlusturulmaTarihi,gonderiGuncellenmeTarihi,gonderiYayinlanmaTarihi,gonderiIcerik")] tblGonderi tblGonderi)
        {
            if (ModelState.IsValid)
            {
                db.tblGonderi.Add(tblGonderi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.gonderiKullaniciAdminId = new SelectList(db.tblAdminRol, "adminRolId", "adminRolBaslik", tblGonderi.gonderiKullaniciAdminId);
            ViewBag.gonderiEtiketId = new SelectList(db.tblEtiket, "etiketId", "etiketBaslik", tblGonderi.gonderiEtiketId);
            ViewBag.gonderiKategoriId = new SelectList(db.tblKategori, "tblKategoriId", "tblKategoriBaslik", tblGonderi.gonderiKategoriId);
            ViewBag.gonderiKullaniciId = new SelectList(db.tblKullanici, "kullaniciId", "kullaniciAdSoyad", tblGonderi.gonderiKullaniciId);
            ViewBag.gonderiKullaniciUyelikRolId = new SelectList(db.tblNormalRol, "normalRolId", "normalRolBaslik", tblGonderi.gonderiKullaniciUyelikRolId);
            return View(tblGonderi);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGonderi tblGonderi = db.tblGonderi.Find(id);
            if (tblGonderi == null)
            {
                return HttpNotFound();
            }
            ViewBag.gonderiKullaniciAdminId = new SelectList(db.tblAdminRol, "adminRolId", "adminRolBaslik", tblGonderi.gonderiKullaniciAdminId);
            ViewBag.gonderiEtiketId = new SelectList(db.tblEtiket, "etiketId", "etiketBaslik", tblGonderi.gonderiEtiketId);
            ViewBag.gonderiKategoriId = new SelectList(db.tblKategori, "tblKategoriId", "tblKategoriBaslik", tblGonderi.gonderiKategoriId);
            ViewBag.gonderiKullaniciId = new SelectList(db.tblKullanici, "kullaniciId", "kullaniciAdSoyad", tblGonderi.gonderiKullaniciId);
            ViewBag.gonderiKullaniciUyelikRolId = new SelectList(db.tblNormalRol, "normalRolId", "normalRolBaslik", tblGonderi.gonderiKullaniciUyelikRolId);
            return View(tblGonderi);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gonderiId,gonderiKullaniciId,gonderiKullaniciUyelikRolId,gonderiKullaniciAdminId,gonderiKategoriId,gonderiBaslik,gonderiEtiketId,gonderiOzet,gonderiYayinlanma,gonderiOlusturulmaTarihi,gonderiGuncellenmeTarihi,gonderiYayinlanmaTarihi,gonderiIcerik")] tblGonderi tblGonderi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGonderi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.gonderiKullaniciAdminId = new SelectList(db.tblAdminRol, "adminRolId", "adminRolBaslik", tblGonderi.gonderiKullaniciAdminId);
            ViewBag.gonderiEtiketId = new SelectList(db.tblEtiket, "etiketId", "etiketBaslik", tblGonderi.gonderiEtiketId);
            ViewBag.gonderiKategoriId = new SelectList(db.tblKategori, "tblKategoriId", "tblKategoriBaslik", tblGonderi.gonderiKategoriId);
            ViewBag.gonderiKullaniciId = new SelectList(db.tblKullanici, "kullaniciId", "kullaniciAdSoyad", tblGonderi.gonderiKullaniciId);
            ViewBag.gonderiKullaniciUyelikRolId = new SelectList(db.tblNormalRol, "normalRolId", "normalRolBaslik", tblGonderi.gonderiKullaniciUyelikRolId);
            return View(tblGonderi);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGonderi tblGonderi = db.tblGonderi.Find(id);
            if (tblGonderi == null)
            {
                return HttpNotFound();
            }
            return View(tblGonderi);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGonderi tblGonderi = db.tblGonderi.Find(id);
            db.tblGonderi.Remove(tblGonderi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
