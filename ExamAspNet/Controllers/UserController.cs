using ExamAspNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExamAspNet.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private GymContext db = new GymContext();
        public ActionResult Index(string msg = "")
        {
            ViewBag.Msg = msg;
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SignUp([Bind(Include = "Id,Name,Email,Phone")] User user, string cardT)
        {
            if (ModelState.IsValid)
            {
                user.ImageUrl = "https://static.thenounproject.com/png/4912996-200.png";
                user.Status = "Hello! I am a participant of IGNITE gym club";
                user.CardType = (CardType)Enum.Parse(typeof(CardType), cardT);
                user.ClassesNum = user.CardType == CardType.Gold ? 6 : user.CardType == CardType.Silver ? 4 : 2;
                db.Users.Add(user);
                db.SaveChanges();
                
                return RedirectToAction($"LogIn");
            }
            return View(user);

        }

        [HttpPost]
        public ActionResult LogIn(string Email, string Phone)
        {
            User user = db.Users.Where(x => x.Phone == Phone && x.Email == Email).FirstOrDefault();
            if (user == null) {
                ViewBag.Msg = $"User is not found. Check your information.";
                return View(); 
            }
            return RedirectToAction("Index", "Gym", new { UserId = user.Id });
        }

        public ActionResult Details(int? id, string pic = null)
        {
            if (id == null || Session["id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null || Session["id"].ToString() != id.ToString() || Session["name"].ToString() != user.Name)
            {
                return HttpNotFound();
            }

            var temp = user.Practices.ToList();
            List<Practice> practices = new List<Practice>();
            foreach(var t in temp)
            {
                practices.Add(db.Practices.Where(x => x.Id == t.Id).Include(x => x.Coach).Include(x => x.DayOfWeeks).Include(x => x.FitProgram).First());
            }
            ViewBag.Applications = db.Applications.Where(x=> x.UserId== id && x.isAccepted == null).Count();
            ViewBag.Practices = practices;
            return View(user);
        }

        public ActionResult History(int? id)
        {
            if (id == null || Session["id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null || Session["id"].ToString() != id.ToString() || Session["name"].ToString() != user.Name)
            {
                return HttpNotFound();
            }
            ViewBag.User = user;
            ViewBag.Messages = db.Messages.Where(x => x.UserId == user.Id).ToList();
            return PartialView(db.Coaches.ToList());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null || Session["id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null || Session["id"].ToString() != id.ToString() || Session["name"].ToString() != user.Name)
            {
                return HttpNotFound();
            }
            var temp = user.Practices.ToList();
            List<Practice> practices = new List<Practice>();
            foreach (var t in temp)
            {
                practices.Add(db.Practices.Where(x => x.Id == t.Id).Include(x => x.Coach).Include(x => x.DayOfWeeks).Include(x => x.FitProgram).First());
            }
            ViewBag.Practices = practices;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,Status,ImageUrl")] User user,string cardT, int[] selectedPractices)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Msg = "";
                User newuser = db.Users.Find(user.Id);
                if(cardT == "")
                {
                    ViewBag.Msg = "Choose card type";
                    var temp = newuser.Practices.ToList();
                    List<Practice> practices = new List<Practice>();
                    foreach (var t in temp)
                    {
                        practices.Add(db.Practices.Where(x => x.Id == t.Id).Include(x => x.Coach).Include(x => x.DayOfWeeks).Include(x => x.FitProgram).First());
                    }
                    ViewBag.Practices = practices;
                    return View(user);
                }
                newuser.CardType = (CardType)Enum.Parse(typeof(CardType), cardT);
                
                newuser.ClassesNum = newuser.CardType == CardType.Gold ? 6 : newuser.CardType == CardType.Silver ? 4 : 2;

                if(selectedPractices!= null)
                {
                    if (selectedPractices.Length > newuser.ClassesNum)
                    {
                        ViewBag.Msg = $"Too many activities, now your maximum is {newuser.ClassesNum}. Change card type or number of activities";
                        var temp = newuser.Practices.ToList();
                        List<Practice> practices = new List<Practice>();
                        foreach (var t in temp)
                        {
                            practices.Add(db.Practices.Where(x => x.Id == t.Id).Include(x => x.Coach).Include(x => x.DayOfWeeks).Include(x => x.FitProgram).First());
                        }
                        ViewBag.Practices = practices;
                        return View(user);
                    }

                    newuser.Practices.Clear();

                    foreach (var i in db.Practices.Where(co => selectedPractices.Contains(co.Id)))
                    {
                        newuser.Practices.Add(i);
                    }
                }
                else
                { newuser.Practices.Clear();}
                

                if (user.Status == null || user.Status == "")
                    newuser.Status = "Hello! I am a participant of IGNITE gym club";
                else
                    newuser.Status = user.Status;

                if (user.ImageUrl == null || user.ImageUrl == "")
                    newuser.ImageUrl = "https://static.thenounproject.com/png/4912996-200.png";
                else
                    newuser.ImageUrl = user.ImageUrl;

                if (newuser.Name != user.Name)
                {
                    newuser.Name = user.Name;
                    Session["name"] = newuser.Name;
                }
                    
                if (newuser.Phone != user.Phone)
                    newuser.Phone = user.Phone;

                if (newuser.Email != user.Email)
                    newuser.Email = user.Email;


                db.Entry(newuser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction($"Details/{user.Id}");
            }
            return View(user);
        }
    }
}