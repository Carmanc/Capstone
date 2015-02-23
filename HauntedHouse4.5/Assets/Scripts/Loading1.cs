using UnityEngine;
using System.Collections;

public class Loading1 : MonoBehaviour {

	//static int Count=0;
	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width/2.5f,Screen.height/3,Screen.width/5,Screen.height/10),"Loading...");
	}
	
	// Use this for initialization
	void Start () {
		//Count=0;
		Application.LoadLevel(5);
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
