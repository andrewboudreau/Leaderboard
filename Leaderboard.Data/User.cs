using System.Collections.Generic;

namespace Leaderboard.Data
{
    public class User
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ICollection<UserScore> Scores { get; set; }
    }
}
