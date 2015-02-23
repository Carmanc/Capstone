using UnityEngine;
using System.Collections;

public class PumpkinMover : MonoBehaviour {

	public float RotateValue;
	public Transform EnemyPunpkin;

	void Update() {
	
		// Let the pimpkin rotate along Y-axis
		transform.Rotate(Vector3.forward * RotateValue, Space.World);

		//EnemyPunpkin.animation.CrossFade("PumpkinJump");
	}
}
