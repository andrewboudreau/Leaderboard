using System;
using System.Data.Entity;

namespace Leaderboard.Data
{
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class LeaderboardContext : DbContext, ILeaderboardContext
    {
        public DbSet<UserScore> UserScores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configure all DateTime properties in my model to map to the datetime2 type in SQL Server instead of datetime
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            // Any property in our model named 'Id' will be configured as the primary key of whatever entity its part of.
            modelBuilder.Properties().Where(x => x.Name == "Id").Configure(x => x.IsKey());
        } 
    }
}
