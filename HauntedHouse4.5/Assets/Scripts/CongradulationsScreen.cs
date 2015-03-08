using UnityEngine;
using System.Collections;

public class CongradulationsScreen : MonoBehaviour {

	static int Count=0;
	void OnGUI()
	{
		string temp = rankMe();
		//							Left			Top				Width			Hight
		GUI.Label(new Rect(Screen.width/2.5f,Screen.height/3,Screen.width/2,Screen.height/10),"You Escaped Flat Rock");

		GUI.Label(new Rect(Screen.width/3.5f,((/*2*/(Screen.height/3)+(Screen.height/3)/3)),Screen.width/1,Screen.height/10),"Bottom Floor Completion Time: "+GameTimer.Instance.getCompletion1Time()+" Seconds");

		GUI.Label(new Rect(Screen.width/3.5f,((/*2*/(Screen.height/3)+2*(Screen.height/3)/3)),Screen.width/1,Screen.height/10),"Second Floor Completion Time: "+GameTimer.Instance.getCompletion2Time()+" Seconds");

		GUI.Label(new Rect(Screen.width/3.5f,((/*2*/(Screen.height/3)+3*(Screen.height/3)/3)),Screen.width/1,Screen.height/10),"Basement Floor Completion Time: "+GameTimer.Instance.getCompletion3Time()+" Seconds");

		GUI.Label(new Rect(Screen.width/3.5f,((/*2*/(Screen.height/3)+4*(Screen.height/3)/3)),Screen.width/1,Screen.height/10),"Your Rank is "+temp);

		GUI.Label(new Rect(Screen.width/3.5f,((/*2*/(Screen.height/3)+15*(Screen.height/3)/3)),Screen.width/1,Screen.height/10),"Press space to go to start menu");

	}

	string rankMe()
	{
		//Currently The rankings will be everybody gets a rank of monster Buster
		double temp = GameTimer.Instance.getCompletion1Time() + GameTimer.Instance.getCompletion2Time() + GameTimer.Instance.getCompletion3Time();
		if(temp < (15*60))
		{
			return "Monster Buster";
		}
		else if(temp < (20*60))
		{
			return "Monster Hunter";
		}
		else if(temp < (25*60))
		{
			return "Ghost Buster";
		}
		else if(temp < (30*60))
		{
			return "Ghost Hunter";
		}
		else if(temp < (35*60))
		{
			return "Ghost Seeker";
		}
		else if(temp < (40*60))
		{
			return "Ghost adventurer";
		}
		else if(temp < (45*60))
		{
			return "Adult with a camcorder";
		}
		else if(temp < (50*60))
		{
			return "kid with a camcorder";
		}
		else if(temp < (55*60))
		{
			return "Completly Lost";
		}
		else
		{
			return "scaredy cat";
		}
	}
	
	// Use this for initialization
	void Start () {
		//Count=0;
	}
	
	// Update is called once per frame
	void Update () {
		/*Count++;
		if(Count==100)
		{
			Application.LoadLevel(0);
		}*/
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel(0);
		}
	}
}
