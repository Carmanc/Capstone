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

		GUI.Label(new Rect(Screen.width/3.5f,((/*2*/(Screen.height/3)+2*(Screen.height/3)/3)),Screen.width/1,Screen.height/10),"Second Floor Completion Time: "+GameTimer.Instance.getCompletion1Time()+" Seconds");

		GUI.Label(new Rect(Screen.width/3.5f,((/*2*/(Screen.height/3)+3*(Screen.height/3)/3)),Screen.width/1,Screen.height/10),"Basement Floor Completion Time: "+GameTimer.Instance.getCompletion1Time()+" Seconds");

		GUI.Label(new Rect(Screen.width/3.5f,((/*2*/(Screen.height/3)+4*(Screen.height/3)/3)),Screen.width/1,Screen.height/10),"Your Rank is "+temp);



	}

	string rankMe()
	{
		//Currently The rankings will be everybody gets a rank of monster Buster
		return "Monster Buster";
	}
	
	// Use this for initialization
	void Start () {
		Count=0;
	}
	
	// Update is called once per frame
	void Update () {
		/*Count++;
		if(Count==100)
		{
			Application.LoadLevel(0);
		}*/
	}
}
