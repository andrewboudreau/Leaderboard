using System.Data.Entity;

namespace Leaderboard.Data
{
    public interface ILeaderboardContext
    {
        DbSet<User> Users { get; set; }

        DbSet<UserScore> UserScores { get; set; }
    }
}
