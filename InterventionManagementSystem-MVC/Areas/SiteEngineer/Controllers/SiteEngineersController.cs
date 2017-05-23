using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InterventionManagementSystem_MVC.Areas.SiteEngineer.Models;

namespace InterventionManagementSystem_MVC.Areas.SiteEngineer.Controllers
{
    public class SiteEngineersController : Controller
    {
        private EngineerDbContext db = new EngineerDbContext();

        // GET: SiteEngineer/SiteEngineers
        public ActionResult Index()
        {
            return View(db.SiteEngineers.ToList());
        }

        // GET: SiteEngineer/SiteEngineers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteEngineer siteEngineer = db.SiteEngineers.Find(id);
            if (siteEngineer == null)
            {
                return HttpNotFound();
            }
            return View(siteEngineer);
        }

             public ActionResult Create()
        {
            return View();
        }


        // GET: SiteEngineer/SiteEngineers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteEngineer/SiteEngineers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DistrictName,AuthorisedHours,AuthorisedCosts")] SiteEngineer siteEngineer)
        {
            if (ModelState.IsValid)
            {
                db.SiteEngineers.Add(siteEngineer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteEngineer);
        }

        // GET: SiteEngineer/SiteEngineers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteEngineer siteEngineer = db.SiteEngineers.Find(id);
            if (siteEngineer == null)
            {
                return HttpNotFound();
            }
            return View(siteEngineer);
        }

        // POST: SiteEngineer/SiteEngineers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DistrictName,AuthorisedHours,AuthorisedCosts")] SiteEngineer siteEngineer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteEngineer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteEngineer);
        }

        // GET: SiteEngineer/SiteEngineers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteEngineer siteEngineer = db.SiteEngineers.Find(id);
            if (siteEngineer == null)
            {
                return HttpNotFound();
            }
            return View(siteEngineer);
        }

        // POST: SiteEngineer/SiteEngineers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteEngineer siteEngineer = db.SiteEngineers.Find(id);
            db.SiteEngineers.Remove(siteEngineer);
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
