using System;
using System.Collections.Generic;

namespace Demo.Services.DataContracts
{
	public class AllScoresResponse
	{
		public List<UserScore> Scores;
	}

	public class UserScore
	{
		public int Id;
		public string UserName;
		public int Score;
		public string Created;
	}
}

