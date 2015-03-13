using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	public Transform transform;
	public float moveSpeed;
	public bool canWalk;
	public AnimationClip run;

	public float maxDistance;
	public float coolDownTimer;
	public PlayerHealthBar ph;    //access playerHealthBar.cs
	
	public int damage;
	
	private Transform myTransform;
	public Transform target;
	
	public AnimationClip attack;

	//public float Speed;
	//public Vector3 Target;
	//public Vector3 MoveDirection;
	private int rotationSpeed = 1;



	// Use this for initialization
	void Start () 
	{
		canWalk = true;

		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		myTransform = transform;
		maxDistance = 3;
		coolDownTimer = 0;
		damage = -5;
		
		ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canWalk == true) 
		{
			transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
			animation.CrossFade(run.name);
		}

		float distance = Vector3.Distance (target.position, myTransform.position);
		if (distance < maxDistance) 
		{
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



	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Wall") 
		{
			Debug.Log ("w");
			canWalk=false;
			for (int i = 0; i < 60; i++)
			{
				Quaternion newRotation = Quaternion.AngleAxis (90, Vector3.up);
				transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, .05f);
			}
			canWalk=true;
			//walk forward
			//transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
		}
	}

	void attackPlayer ()
	{
		canWalk = false;
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
