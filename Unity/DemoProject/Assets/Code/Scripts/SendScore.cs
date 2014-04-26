using UnityEngine;
using System.Collections;
using Demo.Services;
using Demo.Services.DataContracts;
using System;

public class SendScore : MonoBehaviour 
{
	void OnGUI()
	{
		if(GUI.Button(new Rect(20,100,150,20),  "Send Score"))
		{
			SaveScore();
		}
	}

	private void SaveScore()
	{
		DemoScene scene = this.gameObject.GetComponent<DemoScene>();
		int score = scene.score;
		
		//send the current score of the game
		SendGameScore(score);
	}
	
	private void SendGameScore(int score)
	{
		string userName = "test";
		string password = "test123";

		//Here we will first call the auth service to grab an auth token,
		//and then call the game score service once it returns.
		Debug.Log("Sending game score");
		LeaderboardService service = new LeaderboardService();
		service.AuthenticateUser(userName, password, (object obj) => 
			{ 
				AuthResponse resp = (AuthResponse)obj;
				
				if(!String.IsNullOrEmpty(resp.access_token))
				{
					service.SaveScore(score, GameScoreResponse); 
				}
			});
	}


	private void GameScoreResponse(object obj)
	{
		SaveScoreResponse data = (SaveScoreResponse)obj;

		Debug.Log(String.Format("Score data saved for user:{0}, score: {1}", data.UserName, data.Score));
	}
	
}
