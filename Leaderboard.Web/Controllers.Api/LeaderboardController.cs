using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

using Leaderboard.Data;
using System.Web.Http.Cors;

namespace Leaderboard.Web.Controllers.Api
{

    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LeaderboardController : ApiController
    {
        private LeaderboardContext db = new LeaderboardContext();

        public IQueryable<UserScore> GetUserScores()
        {
            return this.db.UserScores;
        }

        [ResponseType(typeof(UserScore))]
        public IHttpActionResult GetUserScore(int id)
        {
            UserScore userscore = this.db.UserScores.Find(id);
            if (userscore == null)
            {
                return this.NotFound();
            }

            return this.Ok(userscore);
        }

        [ResponseType(typeof(UserScore))]
        public IHttpActionResult PostUserScore(UserScore userscore)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.db.UserScores.Add(userscore);
            this.db.SaveChanges();

            return this.CreatedAtRoute("DefaultApi", new { id = userscore.Id }, userscore);
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