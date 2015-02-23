using UnityEngine;
using System.Collections;

public class PumpkinAttack : MonoBehaviour {

	public float maxDistance;
	public float coolDownTimer;
	public PlayerHealthBar ph;    //access playerHealthBar.cs
	
	public int damage;
	
	private Transform myTransform;
	public Transform target;
	

	// Use this for initialization
	void Start () 
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		myTransform = transform;
		maxDistance = 3;
		coolDownTimer = 0;
		damage = -1;
		
		ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distance = Vector3.Distance (target.position, myTransform.position);
		if (distance < maxDistance) 
		{
			attackPlayer ();
		
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
		//Vector3 dir = Vector3.Normalize(target.position - myTransform.position);
		//float direction = Vector3.Dot (dir, transform.forward);
		if (coolDownTimer == 0) 
			{
				ph.ChangeHealth (damage);
				coolDownTimer = 3;
			}

		
		
	}
}
