using UnityEngine;
using System.Collections;
using System;
using System.Text;

/// <summary>
/// This class represents a WWW Call that is non blocking
/// </summary>
public class WWWAsync : MonoBehaviour 
{	
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Space))
		{
			AuthAsync();
		}
	}
	
	private void AuthAsync()
	{
		string userName = "test";
		string password = "test123";
		
		string url = "http://leaderboard-web.azurewebsites.net/Token";
		string message = String.Format("userName={0}&password={1}&grant_type=password", userName, password);

		//Transform our text into a byte array
		UTF8Encoding encoding = new UTF8Encoding();
		byte[] messageBytes = encoding.GetBytes(message);

		//Setup our headers for the auth call
		Hashtable headers = new Hashtable();
		headers.Add("Content-Type", "application/x-www-form-urlencoded");

		//Make the service call
		StartCoroutine(WWWRequestAsync(url, messageBytes, headers));
	}

	/// <summary>
	/// Coroutine that will let the application continue running while the WWW class is busy
	/// </summary>
	private IEnumerator WWWRequestAsync(string url, byte[] messageBytes, Hashtable headers)
	{
		WWW auth = new WWW(url, messageBytes, headers);
		
		while(!auth.isDone)
		{
			yield return auth;
		}
		
		string response = auth.text;
		
		Debug.Log(response);
	}
}
