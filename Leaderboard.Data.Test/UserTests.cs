using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;

namespace Leaderboard.Data.Test
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser()
        {
            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Create();
                context.Users.Add(user);
                user.Name = Guid.NewGuid().ToString();

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void CreateThenReadUser()
        {
            int userId;
            string userName;
            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Create();
                context.Users.Add(user);
                user.Name = Guid.NewGuid().ToString();

                context.SaveChanges();

                userId = user.Id;
                userName = user.Name;
            }

            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Single(u => u.Id == userId);
                Assert.AreEqual(userId, user.Id);
                Assert.AreEqual(userName, user.Name);
            }
        }

        [TestMethod]
        public void CreateThenFindUser()
        {
            int userId;
            string userName;
            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Create();
                context.Users.Add(user);
                user.Name = Guid.NewGuid().ToString();

                context.SaveChanges();

                userId = user.Id;
                userName = user.Name;
            }

            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Find(userId);
                Assert.AreEqual(userId, user.Id);
                Assert.AreEqual(userName, user.Name);
            }
        }

        [TestMethod]
        public void CreateThenDeleteUser()
        {
            int userId;
            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Create();
                context.Users.Add(user);
                user.Name = Guid.NewGuid().ToString();

                context.SaveChanges();

                userId = user.Id;
            }

            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Single(u => u.Id == userId);
                context.Users.Remove(user);
                context.SaveChanges();
            }
            
            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Find(userId);
                Assert.IsNull(user);
            }
        }

        [TestMethod]
        public void CreateThenUpdateUser()
        {
            int userId;
            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Create();
                context.Users.Add(user);
                user.Name = Guid.NewGuid().ToString();

                context.SaveChanges();

                userId = user.Id;
            }

            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Single(u => u.Id == userId);
                user.Name = "foo";
                context.SaveChanges();
            }

            using (var context = new LeaderboardContext())
            {
                var user = context.Users.Find(userId);
                Assert.AreEqual(userId, user.Id);
                Assert.AreEqual("foo", user.Name);
            }
        }
    }
}
