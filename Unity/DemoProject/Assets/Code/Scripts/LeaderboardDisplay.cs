using UnityEngine;
using System.Collections;
using Demo.Services;
using Demo.Services.DataContracts;
using System;
using System.Text;
using System.Linq;

public class LeaderboardDisplay : MonoBehaviour 
{
	public bool Displayed
	{
		get { return showLeaderboardData; }
	}

	bool showLeaderboardData = false;

	Vector2 scrollViewVector = Vector2.zero;

	string leaderboardText = null;
	int lineCount = 0;
	int fontSize = 16;

	public void GetLeaderboardData()
	{
		LeaderboardService service = new LeaderboardService();
		if(LeaderboardService.Authenticated)
		{
			service.GetLeaderboard(LeaderboardResponse);
		}
		else
		{
			string userName = DemoScene.userName;
			string password = DemoScene.password;

			service.AuthenticateUser(userName, password, (object obj) => 
			{ 
				AuthResponse resp = (AuthResponse)obj;
				
				if(!String.IsNullOrEmpty(resp.access_token))
				{
					service.GetLeaderboard(LeaderboardResponse);
				}
			});
		}
	}

	private void LeaderboardResponse(object resp)
	{
		AllScoresResponse scores = (AllScoresResponse)resp;
		showLeaderboardData = true;

		lineCount = 0;
		scrollViewVector = Vector2.zero;

		StringBuilder builder = new StringBuilder();
		if(scores.Scores != null)
		{
			var highScores = scores.Scores
				.GroupBy(s => s.UserName)
					.Select(g => new UserScore(){
						UserName = g.Key,
						Score = g.Max (x => x.Score)
					})
					.OrderBy(s => s.Score);

			foreach(UserScore score in highScores)
			{
				builder.AppendLine(String.Format("{0} {1}", score.UserName, score.Score));
				lineCount++;
			}
		}

		leaderboardText = builder.ToString();
	}

	void OnGUI()
	{
		if(showLeaderboardData && leaderboardText != null)
		{
			float height = lineCount * fontSize;

			scrollViewVector = GUI.BeginScrollView (new Rect (25, 140, 400, 400), scrollViewVector,new Rect (0, 0, 400, height));

			leaderboardText = GUI.TextArea (new Rect (0, 0, 400, height), leaderboardText);

			GUI.EndScrollView();
		}
	}
	
	public void Hide()
	{
		leaderboardText = null;
		showLeaderboardData = false;
	}

	void Update()
	{
		if(Input.GetKeyUp(KeyCode.L))
		{
			GetLeaderboardData();
		}
	}
}
