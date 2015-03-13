using UnityEngine;
using System.Collections;

public class HealthLevelTransfer : MonoBehaviour {

	private static HealthLevelTransfer _instance;
	int health;
	//Construct  
	private HealthLevelTransfer(){
		health=100;
	}

	//  Instance 
	public static HealthLevelTransfer Instance
	{
		get{
			if(_instance == null)
			{
				_instance=GameObject.FindObjectOfType(typeof(HealthLevelTransfer)) as HealthLevelTransfer; 
				
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
	
	public int getHealth()
	{
		return health;
	}
	public void setHealth(int newHealth)
	{
		health=newHealth;
	}
	public void resetHealth()
	{
		health=100;
	}
}
