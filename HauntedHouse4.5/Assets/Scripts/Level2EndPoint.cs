using UnityEngine;
using System.Collections;

public class Level2EndPoint : MonoBehaviour {

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
			double temp =System.DateTime.Now.Millisecond;
			GameTimer.Instance.setEnd2(temp);
			Application.LoadLevel(3);
		}
		
	}
	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log("isWorking2");
			double temp =System.DateTime.Now.Millisecond;
			GameTimer.Instance.setEnd2(temp);
			Application.LoadLevel(3);
		}
	}
}

