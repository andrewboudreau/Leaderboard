using System;
using System.Collections;

namespace Demo.Services.DataContracts
{
	public class AuthResponse 
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int expires_in { get; set; }
		public string userName { get; set; }
	}
}