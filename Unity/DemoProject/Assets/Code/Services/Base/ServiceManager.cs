using System;
using UnityEngine;

namespace Demo.Services
{
	/// <summary>
	/// This class is used to create and hold and instance to a gameobject 
	/// which can be used to host coroutines for the service classes
	/// </summary>
	public class ServiceManager : MonoBehaviour
	{
		private static ServiceManager instance = null;
		public static ServiceManager Instance
		{
			get
			{
				if(instance == null)
				{
					instance = FindObjectOfType(typeof(ServiceManager)) as ServiceManager;
					
					if(instance == null)
					{
						GameObject obj = new GameObject("ServiceManager");
						DontDestroyOnLoad(obj); //Make sure this object persists across scenes
						instance = obj.AddComponent<ServiceManager>();
					}
				}
				
				return instance;
			}
		}
		
		
		void OnApplicationQuit()
		{
			//null out the reference when the application quits
			instance = null;	
		}
		
	}
}