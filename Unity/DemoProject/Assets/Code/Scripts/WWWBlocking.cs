using UnityEngine;
using System.Collections;
using Demo.Services;
using Demo.Services.DataContracts;
using System;
using System.Text;

/// <summary>
/// This class demonstrates a WWW call which will block until it is finished
/// </summary>
public class WWWBlocking : MonoBehaviour 
{
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Space))
		{
			AuthBlocking();
		}
	}
	
	private void AuthBlocking()
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
		WWW auth = new WWW(url, messageBytes, headers);

		while(!auth.isDone)
		{
			//Do nothing, we have to wait until the www class is done to get its response data
		}

		string response = auth.text;

		Debug.Log(response);
	}
	

}
