using UnityEngine;
using System.Collections;

public class CandyMover : MonoBehaviour {

	public float tumble;
	
	void Start ()
	{
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
