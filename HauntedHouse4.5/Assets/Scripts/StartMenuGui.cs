using UnityEngine;
using System.Collections;

public class StartMenuGui : MonoBehaviour {
	private bool reset = false;
	void start()
	{
		GameTimer.Instance.resetTimers();
		HealthLevelTransfer.Instance.resetHealth();
		//Screen.showCursor = true;
		//reset=false;
	}
	void OnGUI()
	{
		//							Left			Top				Width			Hight
		if(GUI.Button(new Rect(Screen.width/3.5f,Screen.height/3,Screen.width/2,Screen.height/10),"Options"))
		{
			Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(Screen.width/3.5f,Screen.height/2,Screen.width/2,Screen.height/10),"Quit"))
		{
			Application.Quit();
		}
		if(GUI.Button(new Rect(Screen.width/3.5f,Screen.height/5,Screen.width/2,Screen.height/10),"Start"))
		{
			Screen.showCursor = false;
			Application.LoadLevel(4);
		}
	}
	void update()
	{
		Screen.lockCursor = true;
		Screen.showCursor = false;
		/*if(!reset)
		{
			resetTimers();
			GameTimer.Instance.reset=true;

		}*/
	}

}
