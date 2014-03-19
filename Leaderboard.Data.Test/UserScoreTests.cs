using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace Leaderboard.Data.Test
{
    [TestClass]
    public class UserScoreTests
    {
        [TestMethod]
        public void CreateUserAddScore()
        {
            int userId;
            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Create();
                context.Users.Add(user);
                user.Name = Guid.NewGuid().ToString();

                var score = context.UserScores.Create();
                context.UserScores.Add(score);
                score.Score = 10;
                score.User = user;
                score.Created = DateTime.UtcNow;

                context.SaveChanges();

                userId = user.Id;
            }

            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Single(u => u.Id == userId);
                
                Assert.AreEqual(userId, user.Id);
                Assert.AreEqual(1, user.Scores.Count(), "User should have 1 score");
                Assert.AreEqual(10, user.Scores.Single().Score, "User should have a score of 10");
            }
        }
    }
}
