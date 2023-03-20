using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using ExamAspNet.Models;
using Microsoft.Ajax.Utilities;

namespace ExamAspNet.Controllers
{
    public class GymController : Controller
    {
        private GymContext db = new GymContext();
        public List<string> Pics1 = new List<string>() {"https://images.squarespace-cdn.com/content/v1/555d1abee4b018e272bccf5c/1617802561456-169AYKO0MVJ8QSTXJXUL/Page-One-Pilates-Chicago-West-Loop-Photographer-Melody-Joy-Co-75.jpg?format=750w", "https://images.squarespace-cdn.com/content/v1/555d1abee4b018e272bccf5c/1617805691258-HXVW762RPNQAK21UFYX5/Page-One-Pilates-Chicago-West-Loop-Photographer-Melody-Joy-Co-125.jpg?format=750w", "https://i.pinimg.com/564x/a6/41/5c/a6415cff53d0049c549efee281cbb43c.jpg", "https://i.pinimg.com/236x/94/9c/6b/949c6b2a5681c30b2e557a6bdd88c34f.jpg", "https://i.pinimg.com/564x/3e/63/db/3e63db1ac7e79454f931ac6132a4208b.jpg", "https://i.pinimg.com/564x/d4/e6/25/d4e625b4aba2368967ba0d5374b3bdce.jpg" };
        public List<string> Pics = new List<string>() { "https://media.gettyimages.com/id/1326071979/photo/brunette-girl-doing-pilates-exercise-for-the-oblique-abdominals.jpg?s=2048x2048&w=gi&k=20&c=XfFJb7YmNY1zkpTk3fziRSzZ8C-FKlAd6OSF4UUZdkQ=", "https://media.gettyimages.com/id/1189331298/photo/be-mindful-of-how-awesome-you-are.jpg?s=2048x2048&w=gi&k=20&c=wezWoo1o1xfvL6zKTyW2QNkVfdOkyJz4QiPIIZKRemw=", "https://media.gettyimages.com/id/1433136266/photo/people-doing-cross-training-in-an-health-club.jpg?s=2048x2048&w=gi&k=20&c=Ko2Lfc6pE-HA8n4CSpnnrr-osMkSMtG37n6W0fq6Rrw=", "https://media.gettyimages.com/id/1397575319/photo/stretching-out-in-fitness-class.jpg?s=2048x2048&w=gi&k=20&c=RouEKYgLTxn2wM29aCDfgxMuZQa3HUrJ7PS5jJm39CI=", "https://media.gettyimages.com/id/641794674/photo/fitness-people-working-out-with-battle-ropes.jpg?s=2048x2048&w=gi&k=20&c=gp-7n9v39PrllWeRIIaNS9JZ1iTHcM0hWUFzIBD0GTk=", "https://media.gettyimages.com/id/1287929278/photo/not-your-average-classroom.jpg?s=2048x2048&w=gi&k=20&c=HALqISdVdcuf8vxadrQ1D2_OlLJbUPaauyXmSFNX7Vk=" };
        public ActionResult FitProgramIndex(string msg = "")
        {
            ViewBag.Msg = msg;
            return View(db.FitPrograms.ToList());
        }

        public ActionResult CoachIndex()
        {
            return View(db.Coaches.ToList());
        }

        public ActionResult View1()
        {
            return View();
        }

        public ActionResult Index(int UserId = 0)
        {
            if(UserId == 0)
            {
                Session["id"] = null;
                Session["name"] = null;
                return View();
            }
            Session["id"] = UserId.ToString();
            Session["name"] = db.Users.Find(UserId).Name;
            return View();
        }

        public ActionResult CoachDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            var idF = coach.Practices.Select(x => x.FitProgramId);
            List<FitProgram> list = new List<FitProgram>();
            foreach(var i in idF)
            {
                list.Add(db.FitPrograms.Where(x => x.Id == i).First());
            }
            ViewBag.FitPrograms = list;
            return PartialView(coach);
        }

        public ActionResult FitProgramDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FitProgram fitProgram = db.FitPrograms.Find(id);
            if (fitProgram == null)
            {
                return HttpNotFound();
            }
            ViewBag.Picture = Pics[(int)id - 1];

            var idC = fitProgram.Practices.Select(x => x.CoachId);
            List<Coach> list = new List<Coach>();
            foreach (var i in idC)
            {
                list.Add(db.Coaches.Where(x => x.Id == i).First());
            }
            ViewBag.Coaches = list;

            var idF = fitProgram.Practices.Select(x => x.DayOfWeeks).ToList();
            List<Day>days= new List<Day>();
            foreach (var day in idF)
                foreach (var elem in day)
                    days.Add(elem);

            ViewBag.Days = days.OrderBy(x=> x.DayOfWeek);
            return View(fitProgram);
        }

        public ActionResult FitProgramJoin(int? id)
        {
            if (Session["id"] == null)
                return RedirectToAction("Index", "User", new {msg = "Before joining, You need to Log In or Sign up"});
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FitProgram fitProgram = db.FitPrograms.Find(id);
            if (fitProgram == null)
            {
                return HttpNotFound();
            }
            else
            {
                int UserId = int.Parse(Session["id"].ToString());
                User user = db.Users.Find(UserId);
                ViewBag.User = user;
                ViewBag.Practices = db.Practices.Where(x => x.FitProgramId == fitProgram.Id).Include(x => x.Coach).ToList();
                return View(fitProgram);
            }  
        }

        [HttpPost]
        public ActionResult FitProgramJoin(FitProgram fitProgram, int UserId, int[] selectedPractices)
        {
            FitProgram newFit = db.FitPrograms.Find(fitProgram.Id);
            User user = db.Users.Find(UserId);
            if (Session["id"] == null)
                Session["id"] = UserId.ToString();

            if(selectedPractices == null)
            {
                ViewBag.User = user;
                ViewBag.Practices = db.Practices.Where(x => x.FitProgramId == fitProgram.Id).Include(x => x.Coach).ToList();
                ViewBag.Message = "Choose Practice";
                return View(fitProgram);
            }
            if (user.ClassesNum > user.Practices.Count)
            {
                foreach(var prId in selectedPractices)
                {
                    Practice practice = db.Practices.Find(prId);
                    if(user.Practices.Contains(practice))
                        return RedirectToAction("FitProgramIndex", new { msg = "You've already joined that FitProgram" });
                }


                foreach (var prId in selectedPractices)
                {
                    Practice practice = db.Practices.Find(prId);

                    Application application = new Application() { UserId = user.Id, PracticeId = prId, PracticeName = practice.FitProgram.Title, UserName = user.Name };
                    db.Applications.Add(application);
                    db.SaveChanges();
                }
                
                //foreach (var prId in selectedPractices)
                //{
                //    Practice practice = db.Practices.Find(prId);
                //    user.Practices.Add(practice);
                //    db.Entry(practice).State = EntityState.Modified;
                //}
                //db.Entry(user).State = EntityState.Modified;

                //db.SaveChanges();
                return RedirectToAction("FitProgramIndex");
            }
                return RedirectToAction("FitProgramIndex", new { msg = "You've reached the full record on your Card" });
            
        }

        public ActionResult UserPartial()
        {
            return PartialView();
        }

        public ActionResult Schedule()
        {
            ViewBag.Days = db.Days.OrderBy(x=> x.DayOfWeek).ToList();

            return View(db.Practices.Include(x=> x.Coach).Include(x=> x.FitProgram).OrderBy(x=>x.StartTime).ToList());
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
