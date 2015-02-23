using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	public float speed; 	//define his speed
	public float chaseRange = 8; 
	public float attackRange = 2;
	// range of attacts
	//public bool inRange = true;
	public CharacterController controller;
	public Transform player;

	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip attack;
	
	void Awake()
	{
		player = GameObject.FindWithTag("Player").transform;
	}
	// Use this for initialization
	void Start () {
		//health = 100;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (inRange ());
		if (!inRange1 ()) 
		{
			chase ();
	
		} else 
		{
			animation.CrossFade(idle.name);
		}

	}

	bool inRange1()
	{
		if (Vector3.Distance (transform.position, player.position) < chaseRange && Vector3.Distance (transform.position, player.position) > attackRange) 
		{
			return true;
		} 
		else 
		{
			return false;
		}
		//return Vector3.Distance (transform.position, player.position) < range;
	}

	/*bool inRange2()
	{
		if (Vector3.Distance (transform.position, player.position) < attackRange) 
		{
			return true;
		} 
		else 
		{
			return false;
		}
		//return Vector3.Distance (transform.position, player.position) < range;
	}*/

	/*public void getHit(int Thedamage)
	{
		health = health - Thedamage;
	}*/

	void chase()
	{
		transform.LookAt (player.position);
		controller.SimpleMove(transform.forward * speed);
		//animation.CrossFade(run.name);
		//animation.Play(attack.name);
		animation.Play(attack.name);
	}


}
