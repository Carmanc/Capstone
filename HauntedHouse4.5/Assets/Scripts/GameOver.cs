using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	static int Count=0;
	bool drawGUI = true;

	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width/4, Screen.height/2, 250, 30), "Press 'Space' to restart the GAME!");

		if(Input.GetKeyDown (KeyCode.Space))
		{

			Application.LoadLevel(0);
		}

	}

}
