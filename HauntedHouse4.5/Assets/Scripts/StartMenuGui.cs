using UnityEngine;
using System.Collections;

public class StartMenuGui : MonoBehaviour {

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
			Application.LoadLevel(4);
		}
	}
}
