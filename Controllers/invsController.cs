using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AT.Models;
using AT.ViewModel;

namespace AT.Controllers
{
    public class invsController : Controller
    {
        private asset_dbEntities db = new asset_dbEntities();

        // GET: invs
        public ActionResult Index()
        {
            var invs = db.invs.Include(i => i.employee);

            return View(new InvCRUD { createInv = new inv(), readInv = invs.ToList() });
        }
         

        // GET: invs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv inv = db.invs.Find(id);
            if (inv == null)
            {
                return HttpNotFound();
            }
            return PartialView(inv);
        }

        // GET: invs/Create
        public ActionResult Create()
        {
            ViewBag.contact = new SelectList(db.employees, "id", "first_name");
            return PartialView();
        }

        // POST: invs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,inv_num,purchase,cost,description,location,contact")] inv inv)
        {
            if (ModelState.IsValid)
            {
                db.invs.Add(inv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.contact = new SelectList(db.employees, "id", "first_name", inv.contact);
            return View(inv);
        }

        // GET: invs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv inv = db.invs.Find(id);
            if (inv == null)
            {
                return HttpNotFound();
            }
            ViewBag.contact = new SelectList(db.employees, "id", "first_name", inv.contact);
            return PartialView(inv);
        }

        // POST: invs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,inv_num,purchase,cost,description,location,contact")] inv inv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.contact = new SelectList(db.employees, "id", "first_name", inv.contact);
            return View(inv);
        }

        // GET: invs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv inv = db.invs.Find(id);
            if (inv == null)
            {
                return HttpNotFound();
            }
            return PartialView(inv);
        }

        // POST: invs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            inv inv = db.invs.Find(id);
            db.invs.Remove(inv);
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
