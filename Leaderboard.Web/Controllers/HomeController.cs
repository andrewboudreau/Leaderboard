using Leaderboard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leaderboard.Web.Controllers
{
    public class HomeController : Controller
    {
        private LeaderboardContext db = new LeaderboardContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Leaderboard Services";
            return View();
        }

        public ActionResult Leaderboard()
        {
            ViewBag.Title = "Leaderboard Screen";
            var scores = this.db.UserScores.ToLookup(k => k.UserName);
            return View(scores);
        }

        public ActionResult WebPlayer()
        {
            ViewBag.Title = "Unity Web Player";
            return View();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
