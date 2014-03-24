using Leaderboard.Data;
using Leaderboard.Web.Models;
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
            var scores = this.db.UserScores
                .GroupBy(m => m.UserName)
                .Select(grp => new UserScoreModel { 
                    UserName = grp.Key, 
                    Score = grp.Max(x => x.Score), 
                    Created = grp.Where(x => x.Score == grp.Max(z => z.Score)).FirstOrDefault().Created 
                })
                .OrderByDescending(s => s.Score)
                .ToList();

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
