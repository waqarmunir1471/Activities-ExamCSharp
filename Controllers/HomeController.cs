using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exam_C.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam_C.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(h=>h.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email","Email Already Exist");
                    return View("Index");
                }
                dbContext.Users.Add(newUser);
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                string pwdHash = Hasher.HashPassword(newUser,newUser.Password);
                newUser.Password = pwdHash;
                HttpContext.Session.SetInt32("UserId",newUser.UserId);
                dbContext.SaveChanges();

                return Redirect("/Dashboard");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            // int? userInDb=(int)HttpContext.Session.GetInt32("UserId");
            User userInDb=dbContext.Users.FirstOrDefault(u=>u.UserId==(int)HttpContext.Session.GetInt32("UserId"));
            List<ActivityCenter> AllActivites = dbContext.Activities.Include(k=>k.ActivityCreator).Include(f=>f.Participants).ThenInclude(l=>l.NavUser).OrderBy(d=>d.ActivityDate).ToList();
            if(userInDb == null)
            {
                return RedirectToAction("Logout");
            }
            else
            {
                ViewBag.User  = userInDb;
                return View(AllActivites);
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLogin newLogin)
        {
            if(ModelState.IsValid)
            {
                var hasher = new PasswordHasher<UserLogin>();
                var signedInUser = dbContext.Users.FirstOrDefault(u => u.Email == newLogin.LoginEmail);
                
                if(signedInUser == null)
                {
                    ViewBag.Message="Email/Password is invalid";
                    return View("Index");
                }
                else
                {
                    var result = hasher.VerifyHashedPassword(newLogin, signedInUser.Password, newLogin.LoginPassword);
                    if(result==0)
                    {
                        ViewBag.Message="Email/Password is invalid";
                        return View("Index");
                    }
                }
                HttpContext.Session.SetInt32("UserId", signedInUser.UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        [HttpGet("CreateActivity")]
        public IActionResult CreateActivity()
        {

            return View("CreateActivity");
        }
        [HttpPost("AddActivity")]
        public IActionResult AddActivity(ActivityCenter newActivity)
        {
            int? UserIdInSession = HttpContext.Session.GetInt32("UserId");
            if(ModelState.IsValid)
            {
                dbContext.Activities.Add(newActivity);
                newActivity.UserId = (int)UserIdInSession;
                dbContext.SaveChanges();
                return Redirect($"/DetailsActivity/{newActivity.ActivityId}");
            }
            else
            {
                return View("CreateActivity");
            }
        }
        [HttpGet("DetailsActivity/{ActivityId}")]
        public IActionResult DetailsActivity(int ActivityId)
        {
            User userInDb=dbContext.Users.FirstOrDefault(u=>u.UserId==(int)HttpContext.Session.GetInt32("UserId"));
            ActivityCenter GetActivity= dbContext.Activities.Include(l=>l.Participants).ThenInclude(l=>l.NavUser).FirstOrDefault(A=>A.ActivityId == ActivityId);
            ViewBag.CoOrdinator = dbContext.Activities.Include(b=>b.ActivityCreator).FirstOrDefault(s=>s.ActivityId == ActivityId);
            // ActivityCenter ActId = dbContext.Activities.Include(k=>k.Participants).ThenInclude(d=>d.NavUser).FirstOrDefault(g=>g.ActivityId == ActivityId);
            // ActivityCenter ActId = dbContext.Activities.FirstOrDefault(g=>g.ActivityId == ActivityId);
            ViewBag.User  = userInDb;

            return View(GetActivity);
        }
        [HttpGet("Delete/{ActivityId}")]
        public IActionResult CancelWedding (int ActivityId)
        {
            ActivityCenter Delete = dbContext.Activities.FirstOrDefault(f=>f.ActivityId == ActivityId);
            dbContext.Activities.Remove(Delete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("Leave/{ActivityId}/{UserId}")]
        public IActionResult NotGoing(int ActivityId, int UserId)
        {
            Invitation CancelInvitation = dbContext.Invitations.FirstOrDefault(d=>d.UserId == UserId && d.ActivityId==ActivityId);
            dbContext.Invitations.Remove(CancelInvitation);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("Join/{ActivityId}/{UserId}")]
        public IActionResult Join(int ActivityId, int UserId)
        {
            Invitation JoinInvitations = new Invitation();
            JoinInvitations.UserId = UserId;
            JoinInvitations.ActivityId = ActivityId;
            dbContext.Invitations.Add(JoinInvitations);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
