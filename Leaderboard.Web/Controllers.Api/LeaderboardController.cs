using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

using Leaderboard.Data;
using System.Web.Http.Cors;
using System.Data.Entity.Core.Objects;

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
            var userscore = this.db.UserScores.Where(s => s.UserName == User.Identity.Name).ToArray();
            if (userscore == null)
            {
                return this.NotFound();
            }

            return this.Ok(userscore);
        }

        [ResponseType(typeof(UserScore))]
        public IHttpActionResult PostUserScore([FromBody] int score)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var userscore = this.db.UserScores.Create();
            userscore.UserName = User.Identity.Name ?? string.Empty;
            userscore.Score = score;
            this.db.UserScores.Add(userscore);
            this.db.SaveChanges();

            return this.Created(this.Request.RequestUri.AbsoluteUri, userscore);
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