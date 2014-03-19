using System;

namespace Leaderboard.Data
{
    public class UserScore
    {
        private DateTime created;

        public UserScore()
        {
            this.created = DateTime.UtcNow;
        }

        public virtual int Id { get; set; }

        public virtual User User { get; set; }

        public virtual int Score { get; set; }

        public virtual DateTime Created
        {
            get
            {
                return this.created;
            }
            set
            {
                this.created = value;
            }
        }
    }
}
