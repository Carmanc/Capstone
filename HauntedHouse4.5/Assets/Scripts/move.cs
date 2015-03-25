using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	public Transform transform;
	public float moveSpeed;
	public bool canWalk;
	public AnimationClip run;

	//public float maxDistance;
	public float coolDownTimer;
	public PlayerHealthBar ph;    //access playerHealthBar.cs
	
	public int damage;
	
	//public Transform myTransform;
	public Transform target1;
	
	public AnimationClip attack;

	//public float Speed;
	//public Vector3 Target;
	//public Vector3 MoveDirection;
	private int rotationSpeed = 1;
	public float smooth = 2.0F;

	public int initialDire = 0;
	public int currentDire;

	public float maxChaseRange = 5.0F;
	public float minChaseRange = 3.0F;
	public float maxAttackRange = 2.9F;

	private int backStepCounter=0;
	private bool backFlag=false;
	// Use this for initialization
	void Start () 
	{
		canWalk = true;

		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target1 = go.transform;
		//myTransform = transform;
		//maxDistance = 3;
		coolDownTimer = 0;
		//damage = -5;
		
		ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));

		currentDire = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canWalk == true) 
		{
			transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
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
		if (inChaseRange ()/* && !Physics.Raycast(transform.localPosition,target1.localPosition,maxChaseRange)*/) {
						chasePlayer ();
			
				} else if (inAttackRange ()) {
						attackPlayer ();
						//Debug.Log ("attack1");
				} else {
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



	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Wall") 
		{
			
			canWalk=false;
			Backstep();
			canWalk=true;
		}
	}

	bool inChaseRange ()
	{
		if (Vector3.Distance (transform.position, target1.position) < maxChaseRange && Vector3.Distance (transform.position, target1.position) > minChaseRange) 
		{
			return true;
		} 
		else 
		{
			return false;
		}
	}

	bool inAttackRange ()
	{
		if (Vector3.Distance (transform.position, target1.position) < maxAttackRange) 
		{
			return true;
		} 
		else 
		{
			return false;
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
				Quaternion target = Quaternion.Euler(0, transform.rotation.y + 90.0f, 0);
				transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 3)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform.rotation.y + 180.0f, 0);
				transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 0)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform.rotation.y + 270.0f, 0);
				transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 1)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform.rotation.y + 0.0f, 0);
				transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
				
			}
		}

	}
	void turnNewDirection()
	{
		if (currentDire == 0)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform.rotation.y + 90.0f, 0);
				transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 1)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform.rotation.y + 180.0f, 0);
				transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 2)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform.rotation.y + 270.0f, 0);
				transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
				
			}
		}
		else if (currentDire == 3)
		{
			for (int i = 0; i < 90; i++)
			{
				Quaternion target = Quaternion.Euler(0, transform.rotation.y + 0.0f, 0);
				transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
				
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
	void chasePlayer ()
	{
		canWalk = false;
		//lookat player
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(target1.position - transform.position), rotationSpeed * Time.deltaTime);
		//move towards player
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		animation.CrossFade(run.name);
	}

	void attackPlayer ()
	{
		Debug.Log ("attack");
		canWalk = false;
		//lookat player
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(target1.position - transform.position), rotationSpeed * Time.deltaTime);
		//move towards player
		//transform.position += transform.forward * moveSpeed * Time.deltaTime;
		Vector3 dir = Vector3.Normalize(target1.position - transform.position);
		float direction = Vector3.Dot (dir, transform.forward);
		if (direction > 0) //enemy is in front of us
		{
			if (coolDownTimer == 0) 
			{
				animation.CrossFade(attack.name);
				ph.ChangeHealth (damage);
				coolDownTimer = 3;
			}
		}
		
		
	}

}
