using UnityEngine;
using System.Collections;

public class Level3EndPoint : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log("isWorking");
			GameTimer.Instance.setEnd3(DateTime.Now);
			Application.LoadLevel(3);
		}
		
	}
	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log("isWorking2");
			GameTimer.Instance.setEnd3(DateTime.Now);
			Application.LoadLevel(3);
		}
	}
}

