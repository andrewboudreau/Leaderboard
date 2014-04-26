using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

using Leaderboard.Data;

namespace Leaderboard.Web.Controllers.Api
{
    public class LeaderboardController : ApiController
    {
        private LeaderboardContext db = new LeaderboardContext();

        public IQueryable<UserScore> GetUserScores()
        {
            return this.db.UserScores;
        }

        [ResponseType(typeof(UserScore[]))]
        [Route("api/Leaderboard/MyScores")]
        public IHttpActionResult GetMyScores()
        {
            var username = User.Identity.Name ?? "anonymous";
            var userscore = this.db.UserScores.Where(s => s.UserName == username).ToArray();

            return this.Ok(userscore);
        }

        [HttpPost]
        [ResponseType(typeof(UserScore))]
        public IHttpActionResult PostUserScore(UserScore score)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var userscore = this.db.UserScores.Create();
            userscore.UserName = User.Identity.Name ?? "anonymous";
            userscore.Score = score.Score;
            this.db.UserScores.Add(userscore);

            this.db.SaveChanges();

            return this.CreatedAtRoute("DefaultApi", new { }, userscore);
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