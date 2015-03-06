using UnityEngine;
using System.Collections;

public class SettingsSingleton : MonoBehaviour {
	//Singleton
	private static SettingsSingleton _instance;
	private float vol;//Volume Controle;
	private float sen;//Mouse sensitivity;
	private bool Pause;
	//Construct  
	private SettingsSingleton(){
		vol=0.5f;
		sen=0.5f;
		Pause=false;
	}

	//  Instance 
	public static SettingsSingleton Instance
	{
		get{
			if(_instance == null)
			{
				_instance=GameObject.FindObjectOfType(typeof(SettingsSingleton)) as SettingsSingleton; 
			
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}
	void Awake()
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}

	public bool changeVolume(float newVolume)
	{
		if(newVolume>=0 && newVolume<=1)
		{
			vol=newVolume;
			return true;
		}
		return false;
	}

	public bool changeSensitivity(float newSensitivity)
	{
		if(newSensitivity>=0 && newSensitivity<=100)
		{
			sen=newSensitivity;
			return true;
		}
		return false;
	}
	public float getVolume()
	{
		return vol;
	}
	public float getSensitivity()
	{
		return sen;
	}
	public void changePause()
	{
		Pause=!Pause;
	}
	public bool getPause()
	{
		return Pause;
	}
}
