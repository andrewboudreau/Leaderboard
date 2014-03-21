using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace Leaderboard.Data.Test
{
    [TestClass]
    public class UserScoreTests
    {
        [TestMethod]
        public void CreateScore()
        {
            int scoreID;
            string name;
            using (var context = new LeaderboardContext())
            {
                var score = context.UserScores.Create();
                context.UserScores.Add(score);
                score.Score = 10;
                score.UserName = "Name" + Guid.NewGuid();
                score.Created = DateTime.UtcNow;

                context.SaveChanges();
                
                name = score.UserName;
                scoreID = score.Id;
            }

            using (var context = new LeaderboardContext())
            {
                var userScore = context.UserScores.Single(s => s.Id == scoreID);

                Assert.AreEqual(scoreID, userScore.Id);
                Assert.AreEqual(name, userScore.UserName);
                Assert.AreEqual(10, userScore.Score, "User should have a score of 10");
            }
        }

        [TestMethod]
        public void CreateMultipleScores()
        {
            var username = "name" + Guid.NewGuid();

            using (var context = new LeaderboardContext())
            {
                for (var i = 0; i < 10; i++)
                {
                    var score = context.UserScores.Create();
                    context.UserScores.Add(score);
                    score.Score = i;
                    score.UserName = username;
                    score.Created = DateTime.UtcNow;
                }

                context.SaveChanges();
            }

            using (var context = new LeaderboardContext())
            {
                var scores = context.UserScores.Where(s => s.UserName == username).ToList();

                Assert.AreEqual(10, scores.Count(), "User should have 10 score");

                for (var i = 0; i < 10; i++)
                {
                    Assert.AreEqual(i, scores.ElementAt(i).Score, "User score should match index");
                }
            }
        }
    }
}
