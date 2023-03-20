using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using ExamAspNet.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ExamAspNet.Controllers
{
    public class AdminController : Controller
    {
        private GymContext db = new GymContext();
        public ActionResult Index(int AdminId = 0, string mes = null)
        {
            if (AdminId == 0)
            {
                Session["id"] = null;
                Session["name"] = null;
                return RedirectToAction("LogIn");
            }
            Session["id"] = AdminId.ToString();
            Session["name"] = db.Admins.Find(AdminId).Name;

            return RedirectToAction("Details", new { id = AdminId, msg = mes });
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string Password)
        {
            Admin admin = db.Admins.Where(x => x.Password == Password).FirstOrDefault();
            if (admin == null)
            {
                ViewBag.Msg = $"admin is not found. Check your information.";
                return View();
            }
            return RedirectToAction("Index", new { AdminId = admin.Id });
        }

        public ActionResult Details(int? id, string msg = null)
        {
            if (id == null || Session["id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            string l = Session["id"].ToString();
            if (admin == null || Session["id"].ToString() != id.ToString() || Session["name"].ToString() != admin.Name)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            ViewBag.Msg = msg;
            return View(db.Applications.Where(x=> x.isAccepted == null));
        }

        public ActionResult Accept(int? id)
        {
            if (id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Application application = db.Applications.Find(id);

            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptConfirmed(int id)
        {
            Models.Application application = db.Applications.Find(id);
            application.isAccepted = true;
            db.Entry(application).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            int adId = int.Parse(Session["id"].ToString());
            if (AddPractice(application.UserId, application.PracticeId))
            {
                Message message = new Message() { SenderId = 100, UserId = application.UserId, Text = $"Your application to join {application.PracticeName} was accepted" };
                db.Messages.Add(message);
                db.SaveChanges();
                
                return RedirectToAction("Index", new { AdminId = adId });
            }
            return RedirectToAction("Index", new {AdminId = adId,mes = "Error"});
        }

        public ActionResult Reject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        [HttpPost, ActionName("Reject")]
        [ValidateAntiForgeryToken]
        public ActionResult RejectConfirmed(int id)
        {
            Models.Application application = db.Applications.Find(id);
            application.isAccepted = false;
            db.Entry(application).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            Message message = new Message() { SenderId = 100, UserId = application.UserId, Text = $"Your application to join {application.PracticeName} was rejected" };
            db.Messages.Add(message);
            db.SaveChanges();
            int adId = int.Parse(Session["id"].ToString());
            return RedirectToAction("Index", new { AdminId = adId});
        }

        public ActionResult AdminPartial()
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

        public bool AddPractice(int userId, int practiceId)
        {
            try
            {
                User user = db.Users.Find(userId);
                Practice practice = db.Practices.Find(practiceId);
                user.Practices.Add(practice);
                db.Entry(practice).State = EntityState.Modified;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}