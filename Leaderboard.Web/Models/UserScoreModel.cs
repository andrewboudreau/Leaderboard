using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leaderboard.Web.Models
{
    public class UserScoreModel
    {
        public string UserName { get; set; }

        public int Score { get; set; }

        public DateTime Created { get; set; }
    }
}