using UnityEngine;
using System.Collections;

public class WallLogic : MonoBehaviour {

	public move move;
	public Transform target;
	public int moveSpeed = 1;

	void Start ()
	{
		GameObject go = GameObject.FindGameObjectWithTag("Enemy");
		target = go.transform;

		
		move = (move)go.GetComponent (typeof(move));
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy") 
		{
			//move.RotationWalk();
			Quaternion newRotation = Quaternion.AngleAxis(180, Vector3.up);
			target.rotation= Quaternion.Slerp(target.rotation, newRotation, .05f);  
			//walk forward
			target.Translate(Vector3.back * Time.deltaTime * moveSpeed);
		}
	}

}
