using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	public GameObject opponent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (opponent);
	}
}
