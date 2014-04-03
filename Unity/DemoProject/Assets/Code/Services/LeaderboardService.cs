using System;
using UnityEngine;
using Demo.Services.DataContracts;
using LitJson;

namespace Demo.Services
{
	public class LeaderboardService : ServiceBase
	{
		private static string userToken = null;

		//In practice we wouldnt want this callback to be static, so we could make parallel service calls, but it works for this simple example.
		private static System.Action<object> callback;

		public void SaveScore(int score, System.Action<object> callback)
		{
			LeaderboardService.callback = callback;

			string url = "http://leaderboard-web.azurewebsites.net/api/leaderboard/";

			SaveScoreRequest request = new SaveScoreRequest();
			request.Score = score;
			string json = JsonMapper.ToJson(request);

			JsonRequestAsync(url, json, userToken, HttpVerb.POST, SaveScoreCallback);
		}

		private void SaveScoreCallback(object obj)
		{
			WWW response = (WWW)obj;

			string json = response.text;
			Debug.Log(json);

			SaveScoreResponse scoreData = JsonMapper.ToObject<SaveScoreResponse>(json);

			if(callback != null)
			{
				callback(scoreData);
			}
		}

		public void AuthenticateUser(string username, string password, System.Action<object> callback)
		{
			LeaderboardService.callback = callback;

			string url = "http://leaderboard-web.azurewebsites.net/Token";
			string message = String.Format("userName={0}&password={1}&grant_type=password", username, password);
			
			RequestAsync(url, message, userToken, HttpVerb.POST, AuthenticationResponse); 
		}


		public void AuthenticationResponse(object obj)
		{
			WWW response = (WWW)obj;

			AuthResponse authResponse = JsonMapper.ToObject<AuthResponse>(response.text);

			//Lets set the user token globally
			userToken = authResponse.access_token;

			if(callback != null)
			{
				callback(authResponse);
			}
		}
	}
}

