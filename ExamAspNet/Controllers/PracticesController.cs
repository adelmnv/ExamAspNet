using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamAspNet.Models;

namespace ExamAspNet.Controllers
{
    public class PracticesController : Controller
    {
        private GymContext db = new GymContext();

        // GET: Practices
        public ActionResult Index()
        {
            var practices = db.Practices.Include(p => p.Coach).Include(p => p.FitProgram);
            return View(practices.ToList());
        }

        // GET: Practices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practice practice = db.Practices.Find(id);
            if (practice == null)
            {
                return HttpNotFound();
            }
            return View(practice);
        }

        // GET: Practices/Create
        public ActionResult Create()
        {
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "Password");
            ViewBag.FitProgramId = new SelectList(db.FitPrograms, "Id", "Title");
            return View();
        }

        // POST: Practices/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CoachId,FitProgramId,StartTime,EndTime")] Practice practice)
        {
            if (ModelState.IsValid)
            {
                db.Practices.Add(practice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "Password", practice.CoachId);
            ViewBag.FitProgramId = new SelectList(db.FitPrograms, "Id", "Title", practice.FitProgramId);
            return View(practice);
        }

        // GET: Practices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practice practice = db.Practices.Find(id);
            if (practice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "Password", practice.CoachId);
            ViewBag.FitProgramId = new SelectList(db.FitPrograms, "Id", "Title", practice.FitProgramId);
            return View(practice);
        }

        // POST: Practices/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CoachId,FitProgramId,StartTime,EndTime")] Practice practice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(practice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "Password", practice.CoachId);
            ViewBag.FitProgramId = new SelectList(db.FitPrograms, "Id", "Title", practice.FitProgramId);
            return View(practice);
        }

        // GET: Practices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practice practice = db.Practices.Find(id);
            if (practice == null)
            {
                return HttpNotFound();
            }
            return View(practice);
        }

        // POST: Practices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Practice practice = db.Practices.Find(id);
            db.Practices.Remove(practice);
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
