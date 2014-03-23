using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Data
{
    public class LeaderboardContextSeed : DropCreateDatabaseAlways<LeaderboardContext>
    {
        protected override void Seed(LeaderboardContext context)
        {
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
