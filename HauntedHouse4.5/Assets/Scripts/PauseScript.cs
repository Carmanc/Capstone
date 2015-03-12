using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	bool isPause = false;
	//float shadeSetting=5.0f;
	float volumeSetting=SettingsSingleton.Instance.getVolume();
	float mouseSetting=SettingsSingleton.Instance.getSensitivity();
	Component cameraScript;
	
	void Start()
	{
		//cameraScript=GetComponent(FirstPersonController) as FirstPersonController;
	}

	void Pause()
	{
		Time.timeScale = 0;
		isPause = true;
		SettingsSingleton.Instance.pauseTrue();
	}
	void unPause()
	{
		Time.timeScale = 1;
		isPause = false;
		SettingsSingleton.Instance.pauseFalse();
	}
	void OnGUI()
	{
		if(isPause)
		{
			//Left				Top				Width			Hight
			/*GUI.Label(new Rect(Screen.width/3.5f,(Screen.height/3)-20,Screen.width/2,Screen.height/10),"Constrast");
			shadeSetting=GUI.HorizontalSlider(new Rect(Screen.width/3.5f,Screen.height/3,Screen.width/2,Screen.height/10),shadeSetting,0.0f,10.0f);*/
			GUI.Label(new Rect(Screen.width/3.5f,(Screen.height/2)-50,Screen.width/2,Screen.height/10),"Volume");
			volumeSetting=GUI.HorizontalSlider(new Rect(Screen.width/3.5f,(Screen.height/2)-30,Screen.width/2,Screen.height/10),volumeSetting,0.0f,1.0f);
			GUI.Label(new Rect(Screen.width/3.5f,(Screen.height/2)-80,Screen.width/2,Screen.height/10),"Moue Sensitivity");
			mouseSetting=GUI.HorizontalSlider(new Rect(Screen.width/3.5f,(Screen.height/2)-60,Screen.width/2,Screen.height/10),mouseSetting,1.0f,10.0f);
			if(GUI.Button(new Rect(Screen.width/3.5f,(Screen.height/2)+10,Screen.width/2,Screen.height/10),"Resume"))
			{
				unPause();
			}
			
		}
	}
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Space))
		{
			if(isPause)
			{
				unPause();
			}
			else
			{
				Pause();
			}

		}
		if(isPause)//SettingsSingleton.Instance.getPause())
		{
			
			
			if(volumeSetting!=SettingsSingleton.Instance.getVolume())
			{
				SettingsSingleton.Instance.changeVolume(volumeSetting);
			}
			if(mouseSetting!=SettingsSingleton.Instance.getSensitivity())
			{
				SettingsSingleton.Instance.changeSensitivity(mouseSetting);
			}
			//SettingsSingleton.Instance.applySound();
		}

	}

}