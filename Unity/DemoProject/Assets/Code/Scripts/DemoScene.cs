using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Demo.Services;

public class DemoScene : MonoBehaviour 
{
	public int score;

	private int maxBugsOnScreen = 2;
	private int bugPoints = 100;

	GameObject bugRoot;
	private List<Bug> bugs = new List<Bug>();

	TextMesh scoreMesh;

	void Awake()
	{
		//Grab the score text object so we can update it
		GameObject score = GameObject.Find("Score");
		scoreMesh = score.GetComponent<TextMesh>();

		//Create a root object to spawn the bugs under.
		bugRoot = new GameObject("BugRoot");
	}

	void Start () 
	{

	}

	void Update () 
	{
		GamePlay();
		RunInput();
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(20,11,100,20),  "Leaderboard"))
		{
			LeaderboardDisplay display = this.GetComponent<LeaderboardDisplay>();
			if(display != null)
			{
				if(display.Displayed)
				{
					display.Hide();
				}
				else
				{
					display.GetLeaderboardData();
				}
			}
		} 
	}
	
	private void GamePlay()
	{
		while(bugs.Count < maxBugsOnScreen)
		{
			Bug bug = Bug.AddRandomBug(bugRoot);
			bug.Offscreen = BugOffScreen;
			bugs.Add(bug);
		}
	}
	
	private void RunInput()
	{
		//Did the user click down
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit))
			{
				//An object was clicked, lets check it out
				GameObject obj = hit.collider.gameObject;
				TestClick(obj);
			}
		}
	}

	/// <summary>
	/// Checks to see if we clicked on a bug
	/// </summary>
	private void TestClick(GameObject obj)
	{
		Bug bug = obj.GetComponent<Bug>();
		
		if(bug != null)
		{
			UpdateScore(bugPoints);

			bugs.Remove(bug);
			bug.Smash();
		}
	}

	/// <summary>
	/// Updates the score internally and on the screen
	/// </summary>
	private void UpdateScore(int points)
	{
		score += points;
		scoreMesh.text = score.ToString();
	}

	/// <summary>
	/// Called when a bug flys too far off the screen
	/// </summary>
	private void BugOffScreen(Bug bug)
	{
		//If the bug went off the screen before we killed it, take away some points
		if(bug.alive)
		{
			UpdateScore(-50);
		}

		bugs.Remove(bug);

		GameObject.Destroy(bug.gameObject);
	}
}
