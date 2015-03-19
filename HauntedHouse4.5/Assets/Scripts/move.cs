using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	public Transform transform1;
	public float moveSpeed;
	public bool canWalk;
	public AnimationClip run;

	public float maxDistance;
	public float coolDownTimer;
	public PlayerHealthBar ph;    //access playerHealthBar.cs
	
	public int damage;
	
	private Transform myTransform;
	public Transform target1;
	
	public AnimationClip attack;

	//public float Speed;
	//public Vector3 Target;
	//public Vector3 MoveDirection;
	private int rotationSpeed = 1;
	public float smooth = 2.0F;

	public int initialDire = 0;
	public int currentDire;
	private int backStepCounter=0;
	private bool backFlag=false;


	// Use this for initialization
	void Start () 
	{
		canWalk = true;

		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target1 = go.transform;
		myTransform = transform;
		maxDistance = 3;
		coolDownTimer = 0;
		damage = -5;
		
		ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));

		currentDire = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canWalk == true) 
		{
			transform1.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
			animation.CrossFade(run.name);
		}
		if(backStepCounter == 10)
		{
			canWalk=false;
			currentDire = pickDirection(currentDire);
			turnNewDirection();
			canWalk=true;
			backFlag=false;
			backStepCounter=0;
		}
		if(backFlag)
		{
			backStepCounter++;
		}
		float distance = Vector3.Distance (target1.position, myTransform.position);
		if (distance < maxDistance) {
						attackPlayer ();
						animation.CrossFade (attack.name);
				} else {
			transform.position = transform1.position;

			canWalk = true;

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

	void Backstep()
	{
		backStepCounter=0;
		backFlag=true;
		if (currentDire == 2)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform1.rotation.y + 90.0f, 0);
				transform1.rotation = Quaternion.Slerp (transform1.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 3)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform1.rotation.y + 180.0f, 0);
				transform1.rotation = Quaternion.Slerp (transform1.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 0)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform1.rotation.y + 270.0f, 0);
				transform1.rotation = Quaternion.Slerp (transform1.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 1)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform1.rotation.y + 0.0f, 0);
				transform1.rotation = Quaternion.Slerp (transform1.rotation, target, Time.deltaTime * smooth);
				
			}
		}

	}
	void turnNewDirection()
	{
		if (currentDire == 0)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform1.rotation.y + 90.0f, 0);
				transform1.rotation = Quaternion.Slerp (transform1.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 1)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform1.rotation.y + 180.0f, 0);
				transform1.rotation = Quaternion.Slerp (transform1.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 2)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform1.rotation.y + 270.0f, 0);
				transform1.rotation = Quaternion.Slerp (transform1.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 3)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform1.rotation.y + 0.0f, 0);
				transform1.rotation = Quaternion.Slerp (transform1.rotation, target, Time.deltaTime * smooth);
				
			}
		}
	}
	int pickDirection(int Last)
	{
		int temp = (int)(Random.value * 1000) %3;
		if(temp == Last)
		{
			temp = 3;
		}
		return temp;
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Wall") 
		{
			Debug.Log ("w");


			canWalk=false;
			Backstep();
			canWalk=true;

			/*canWalk=false;
			currentDire = pickDirection(currentDire);
			turnNewDirection();
			canWalk=true;*/
			//walk forward
			//transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
		}
	}

	void attackPlayer ()
	{
		canWalk = false;
		//lookat player
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation(target1.position - myTransform.position), rotationSpeed * Time.deltaTime);
		//move towards player
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		Vector3 dir = Vector3.Normalize(target1.position - myTransform.position);
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
