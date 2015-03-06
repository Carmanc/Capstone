using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	//Singleton
	private static GameTimer _instance;

	private double timerStart1;
	private double timerEnd1;
	private double timerStart2;
	private double timerEnd2;
	private double timerStart3;
	private double timerEnd3;
	//Construct  
	private GameTimer(){

		timerStart1=0;
		timerEnd1=0;
		timerStart2=0;
		timerEnd2=0;
		timerStart3=0;
		timerEnd3=0;
	}
	
	//  Instance 
	public static GameTimer Instance
	{
		get{
			if(_instance == null)
			{
				_instance=GameObject.FindObjectOfType(typeof(GameTimer)) as GameTimer; 
				
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
	
	public bool setStart1(double start)
	{

		timerStart1=start;
		return true;
	}
	
	public bool setStart2(double start)
	{
		timerStart2=start;
		return true;
	}
	public bool setStart3(double start)
	{
		timerStart3=start;
		return true;
	}
	public bool setEnd1(double end)
	{
		timerEnd1=end;
		return true;
	}
	public bool setEnd2(double end)
	{
		timerEnd2=end;
		return true;
	}
	public bool setEnd3(double end)
	{
		timerEnd3=end;
		return true;

	}
	public void resetTimers()
	{
		timerStart1=0;
		timerEnd1=0;
		timerStart2=0;
		timerEnd2=0;
		timerStart3=0;
		timerEnd3=0;
	}
	public double getCompletion1Time()
	{
		double temp = (timerEnd1-timerStart1)/1000;
		return round(temp, 2);
	}
	public double getCompletion2Time()
	{
		double temp = (timerEnd2-timerStart2)/1000;
		return round(temp, 2);
	}
	public double getCompletion3Time()
	{
		double temp = (timerEnd3-timerStart3)/1000;
		return round(temp, 2);
	}
	double round(double a, int decimalPlaces)
	{
		double powerTen = 1;
		for (int i=0; i<decimalPlaces; i++)
		{
			powerTen=powerTen*10;
			//Debug.Log(powerTen);
		}
		int temp2 = ((int)(a*powerTen));
		double temp = ((double)temp2/powerTen);
		return temp;
	}
}
