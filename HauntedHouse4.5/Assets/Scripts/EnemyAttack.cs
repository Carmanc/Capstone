using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float maxDistance;
	public float coolDownTimer;
	public PlayerHealthBar ph;    //access playerHealthBar.cs

	public int damage;

	private Transform myTransform;
	public Transform target;

	public AnimationClip attack;

	public Transform[] Waypoints;
	public float Speed;
	public int curWayPoint;
	public bool doPatrol = true;
	public Vector3 Target;
	public Vector3 MoveDirection;
	public Vector3 Velocity;
	public AnimationClip run;
	private int moveSpeed = 1;
	private int rotationSpeed = 1;

	// Use this for initialization
	void Start () 
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		myTransform = transform;
		maxDistance = 3;
		coolDownTimer = 0;
		damage = -50;

		ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (curWayPoint < Waypoints.Length) {
			Target = Waypoints [curWayPoint].position;
			animation.CrossFade(run.name);
			MoveDirection = Target - transform.position;
			Velocity = rigidbody.velocity;

			/*if (other.tag == "Wall")
			{
				curWayPoint--;
			}*/
			
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

		float distance = Vector3.Distance (target.position, myTransform.position);
		if (distance < maxDistance) 
		{
			doPatrol = false;
			attackPlayer ();
			animation.CrossFade(attack.name);
		}

		if (coolDownTimer > 0) 
		{
			coolDownTimer = coolDownTimer - Time.deltaTime;
		}

		if (coolDownTimer < 0) 
		{
			coolDownTimer = 0;
		}
	}

	void attackPlayer ()
	{
		//lookat player
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		//move towards player
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		Vector3 dir = Vector3.Normalize(target.position - myTransform.position);
		float direction = Vector3.Dot (dir, transform.forward);
		if (direction > 0) //enemy is in front of us
		{
			if (coolDownTimer == 0) 
			{
				ph.ChangeHealth (damage);
				coolDownTimer = 3;
			}
		}


	}
}
