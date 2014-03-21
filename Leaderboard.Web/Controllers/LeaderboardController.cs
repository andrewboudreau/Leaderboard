using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Leaderboard.Data;

namespace Leaderboard.Web.Controllers
{
    public class LeaderboardController : ApiController
    {
        private LeaderboardContext db = new LeaderboardContext();

        public IQueryable<UserScore> GetUserScores()
        {
            return db.UserScores;
        }

        [ResponseType(typeof(UserScore))]
        public IHttpActionResult GetUserScore(int id)
        {
            UserScore userscore = db.UserScores.Find(id);
            if (userscore == null)
            {
                return NotFound();
            }

            return Ok(userscore);
        }

        [ResponseType(typeof(UserScore))]
        public IHttpActionResult PostUserScore(UserScore userscore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserScores.Add(userscore);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userscore.Id }, userscore);
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