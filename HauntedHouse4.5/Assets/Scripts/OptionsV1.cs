using UnityEngine;
using System.Collections;

public class OptionsV1 : MonoBehaviour {

	float shadeSetting=5.0f;
	float volumeSetting=1.0f;
	float mouseSetting=2.0f;

	void OnGUI()
	{
		/*if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/3,Screen.width/5,Screen.height/10),"Back"))
		{
			Application.LoadLevel(1);
		}*/
		/*if(GUI.HorizontalSlider(new Rect(Screen.width/2.5f,Screen.height/2,Screen.width/5,Screen.height/10),shadeSetting,0.0f,10.0f))
		{
			//Application.LoadLevel(0);
		}*/
													//Left				Top				Width			Hight
		/*GUI.Label(new Rect(Screen.width/3.5f,(Screen.height/3)-20,Screen.width/2,Screen.height/10),"Constrast");
		shadeSetting=GUI.HorizontalSlider(new Rect(Screen.width/3.5f,Screen.height/3,Screen.width/2,Screen.height/10),shadeSetting,0.0f,10.0f);*/
		GUI.Label(new Rect(Screen.width/3.5f,(Screen.height/3)-50,Screen.width/2,Screen.height/10),"Volume");
		volumeSetting=GUI.HorizontalSlider(new Rect(Screen.width/3.5f,(Screen.height/3)-30,Screen.width/2,Screen.height/10),volumeSetting,0.0f,1.0f);
		GUI.Label(new Rect(Screen.width/3.5f,(Screen.height/3)-80,Screen.width/2,Screen.height/10),"Moue Sensitivity");
		mouseSetting=GUI.HorizontalSlider(new Rect(Screen.width/3.5f,(Screen.height/3)-60,Screen.width/2,Screen.height/10),mouseSetting,1.0f,10.0f);

		if(GUI.Button(new Rect(Screen.width/3.5f,Screen.height/2,Screen.width/2,Screen.height/10),"Back Button"))
		{
			Application.LoadLevel(0);
		}

	}
	void Start(){

		volumeSetting=SettingsSingleton.Instance.getVolume();

		mouseSetting=SettingsSingleton.Instance.getSensitivity();
	}
	void Update () {
		if(volumeSetting!=SettingsSingleton.Instance.getVolume())
		{
			SettingsSingleton.Instance.changeVolume(volumeSetting);
			//Debug.Log("V");
		}
		if(mouseSetting!=SettingsSingleton.Instance.getSensitivity())
		{
			SettingsSingleton.Instance.changeSensitivity(mouseSetting);
			//Debug.Log("S");
		}
	}
}
