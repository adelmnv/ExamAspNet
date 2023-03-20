using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using ExamAspNet.Models;

namespace ExamAspNet.Controllers
{
    public class CoachController : Controller
    {
        private GymContext db = new GymContext();

        // GET: Coaches
        public ActionResult Index(int CoachId = 0)
        {
            if (CoachId == 0)
            {
                Session["id"] = null;
                Session["name"] = null;
                return RedirectToAction("LogIn");
            }
            Session["id"] = CoachId.ToString();
            Session["name"] = db.Coaches.Find(CoachId).Name;
            
            return RedirectToAction("Details", new {id = CoachId});
        }

        // GET: Coaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || Session["id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null || Session["id"].ToString() != id.ToString() || Session["name"].ToString() != coach.Name)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            var temp = coach.Practices.ToList();
            List<Practice> practices = new List<Practice>();
            foreach (var t in temp)
            {
                practices.Add(db.Practices.Where(x => x.Id == t.Id).Include(x => x.Users).Include(x => x.FitProgram).First());
            }
            ViewBag.Practices = practices.OrderBy(x=> x.FitProgram.Title);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(practices.OrderBy(x => x.FitProgram.Title));
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string Password)
        {
            Coach coach = db.Coaches.Where(x => x.Password == Password).FirstOrDefault();
            if (coach == null)
            {
                ViewBag.Msg = $"Coach is not found. Check your information.";
                return View();
            }
            return RedirectToAction("Index", new { CoachId = coach.Id });
        }


        public ActionResult Message(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.User = db.Users.Find(id);
            ViewBag.CoachId = Int32.Parse(Session["id"].ToString());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Message([Bind(Include = "SenderId,UserId,Text")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.SenderId = Int32.Parse(Session["id"].ToString());
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index", new { CoachId = message.SenderId});
            }

            return View(message);
        }

        public ActionResult History(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            ViewBag.User = user;
            ViewBag.Coach = Session["name"].ToString();
            int coachid = Int32.Parse(Session["id"].ToString());
            return PartialView(db.Messages.Where(x=> x.UserId == user.Id && x.SenderId == coachid));
        }
        public ActionResult CoachPartial()
        {
            return PartialView();
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
