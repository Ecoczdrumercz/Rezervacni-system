﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlaygroundsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Playgrounds
        public ActionResult Index()
        {
            return View(db.Playgrounds.ToList());
        }

        // GET: Playgrounds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playground playground = db.Playgrounds.Find(id);
            if (playground == null)
            {
                return HttpNotFound();
            }
            return View(playground);
        }

        // GET: Playgrounds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playgrounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Owner")] Playground playground)
        {
            if (ModelState.IsValid)
            {
                db.Playgrounds.Add(playground);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(playground);
        }

        // GET: Playgrounds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playground playground = db.Playgrounds.Find(id);
            if (playground == null)
            {
                return HttpNotFound();
            }
            return View(playground);
        }

        // POST: Playgrounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Owner")] Playground playground)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playground).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playground);
        }

        // GET: Playgrounds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playground playground = db.Playgrounds.Find(id);
            if (playground == null)
            {
                return HttpNotFound();
            }
            return View(playground);
        }

        // POST: Playgrounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Playground playground = db.Playgrounds.Find(id);
            db.Playgrounds.Remove(playground);
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
