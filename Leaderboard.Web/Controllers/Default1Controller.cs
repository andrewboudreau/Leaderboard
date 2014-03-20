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

        // GET api/Default1
        public IQueryable<UserScore> GetUserScores()
        {
            return db.UserScores;
        }

        // GET api/Default1/5
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

        // PUT api/Default1/5
        public IHttpActionResult PutUserScore(int id, UserScore userscore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userscore.Id)
            {
                return BadRequest();
            }

            db.Entry(userscore).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Default1
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

        // DELETE api/Default1/5
        [ResponseType(typeof(UserScore))]
        public IHttpActionResult DeleteUserScore(int id)
        {
            UserScore userscore = db.UserScores.Find(id);
            if (userscore == null)
            {
                return NotFound();
            }

            db.UserScores.Remove(userscore);
            db.SaveChanges();

            return Ok(userscore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserScoreExists(int id)
        {
            return db.UserScores.Count(e => e.Id == id) > 0;
        }
    }
}