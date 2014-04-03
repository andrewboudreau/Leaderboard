using UnityEngine;
using System.Collections;
using System;
using System.Text;

namespace Demo.Services
{
	public class ServiceBase 
	{
		protected void JsonRequestAsync(string url, string json, string userToken, HttpVerb httpVerb, System.Action<object> callback)
		{
			Hashtable headers = new Hashtable();
			headers.Add("Content-Type", "application/json");

			ServiceManager.Instance.StartCoroutine(ProcessRequest(url, json, userToken, httpVerb, headers, callback));                            
		}

		protected void RequestAsync(string url, string json, string userToken, HttpVerb httpVerb, System.Action<object> callback)
		{
			Hashtable headers = new Hashtable();
			headers.Add("Content-Type", "application/x-www-form-urlencoded");

			ServiceManager.Instance.StartCoroutine(ProcessRequest(url, json, userToken, httpVerb, headers, callback));                            
		}

		public IEnumerator ProcessRequest(string url, string json, string userToken, HttpVerb httpVerb, Hashtable headers, System.Action<object> callback)
		{
			headers.Add("Accept", "application/json");

			//Unity's WWW Class doesnt support headers with anything but a post, so we are going to always use a post
			//with a HTTP Method override which the server will parse.
			switch(httpVerb)
			{
			case HttpVerb.PUT:
				headers.Add("X-HTTP-Method-Override","PUT");
				break;
			case HttpVerb.DELETE:
				headers.Add("X-HTTP-Method-Override","DELETE");
				break;
			case HttpVerb.GET:
				headers.Add("X-HTTP-Method-Override","GET");
				break;
			}

			//Lets see if we have an authentication token we want to add
			if(!String.IsNullOrEmpty(userToken))
			{
				headers.Add("Authorization", "Bearer " + userToken);
			}

			UTF8Encoding encoding = new UTF8Encoding();
			byte[] messageBytes = encoding.GetBytes(json);

			//Start the web request and check if it is done
			WWW request = new WWW(url, messageBytes, headers);
			while(!request.isDone)
			{
				yield return request;
			}

			if(callback != null)
			{
				callback(request);
			}
		}
	}

}