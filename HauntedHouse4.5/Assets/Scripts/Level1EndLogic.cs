using UnityEngine;
using System.Collections;

public class Level1EndLogic : MonoBehaviour {
	public GameObject gO;
	// Use this for initialization
	void Start () {
		gO = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log("isWorking");
			GameTimer.Instance.setEnd1(DateTime.Now);

			Application.LoadLevel(3);

		}
		
	}
	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log("isWorking2");
			GameTimer.Instance.setEnd1(DateTime.Now);

			Application.LoadLevel(3);

		}
	}
}

