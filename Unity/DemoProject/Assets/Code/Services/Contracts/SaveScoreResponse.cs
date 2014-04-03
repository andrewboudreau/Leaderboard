using System;

namespace Demo.Services.DataContracts
{
	public class SaveScoreResponse
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public int Score { get; set; }
		public string Created { get; set; }
	}
}

