using System.Data.Entity;

namespace Leaderboard.Data
{
    public interface ILeaderboardContext
    {
        DbSet<UserScore> UserScores { get; set; }
    }
}
