using System;
using System.ComponentModel.DataAnnotations;

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

        public virtual string UserName { get; set; }

        [Required]
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
