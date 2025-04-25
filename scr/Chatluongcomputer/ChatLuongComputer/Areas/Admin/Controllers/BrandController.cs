using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatLuongComputer.Models;

namespace ChatLuongComputer.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        private ChatLuongComputerDbContext db = new ChatLuongComputerDbContext();

        // GET: Admin/Brand
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: Admin/Brand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MBrand mBrand = db.Brands.Find(id);
            if (mBrand == null)
            {
                return HttpNotFound();
            }
            return View(mBrand);
        }

        // GET: Admin/Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image,Link")] MBrand mBrand)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(mBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mBrand);
        }

        // GET: Admin/Brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MBrand mBrand = db.Brands.Find(id);
            if (mBrand == null)
            {
                return HttpNotFound();
            }
            return View(mBrand);
        }

        // POST: Admin/Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,Link")] MBrand mBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mBrand);
        }

        // GET: Admin/Brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MBrand mBrand = db.Brands.Find(id);
            if (mBrand == null)
            {
                return HttpNotFound();
            }
            return View(mBrand);
        }

        // POST: Admin/Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MBrand mBrand = db.Brands.Find(id);
            db.Brands.Remove(mBrand);
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
