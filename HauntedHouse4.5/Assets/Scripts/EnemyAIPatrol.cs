using UnityEngine;
using System.Collections;

public class EnemyAIPatrol : MonoBehaviour {

	public Transform[] Waypoints;
	public float Speed;
	public int curWayPoint;
	public bool doPatrol = true;
	public Vector3 Target;
	public Vector3 MoveDirection;
	public Vector3 Velocity;
	public AnimationClip run;

	void  Update ()
	{
		if (curWayPoint < Waypoints.Length) {
						Target = Waypoints [curWayPoint].position;
			animation.CrossFade(run.name);
						MoveDirection = Target - transform.position;
						Velocity = rigidbody.velocity;

						if (MoveDirection.magnitude < 1) {
								curWayPoint++;
						} else {
								Velocity = MoveDirection.normalized * Speed;
						}
				} else 
		{
			if (doPatrol)
			{
				curWayPoint = 0;
			}
			else
			{
				Velocity = Vector3.zero;
			}
		}

		rigidbody.velocity = Velocity;
		transform.LookAt (Target);
	}


}
