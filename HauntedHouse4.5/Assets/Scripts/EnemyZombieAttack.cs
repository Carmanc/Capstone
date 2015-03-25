using UnityEngine;
using System.Collections;

public class EnemyZombieAttack : MonoBehaviour {

	public float maxDistance;
	public float coolDownTimer;
	public PlayerHealthBar ph;    //access playerHealthBar.cs
	
	public int lifeCount;
	
	private Transform myTransform;
	public Transform target;
	
	public AnimationClip WalkOffset;
	bool drawGUI = false;

	private int moveSpeed = 1;
	private int rotationSpeed = 1;


	// Use this for initialization
	void Start () 
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		myTransform = transform;
		maxDistance = 5;
		coolDownTimer = 0;
		lifeCount = -1;
		
		ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distance = Vector3.Distance (target.position, myTransform.position);

		if (distance < maxDistance && !Physics.Raycast(myTransform.localPosition,target.localPosition,maxDistance)) 
		{
			drawGUI = true;
			attackPlayer ();
			//Debug.Log ("working");
			animation.CrossFade(WalkOffset.name);
		} 
		else
		{
			drawGUI = false;
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
		Vector3 dir = Vector3.Normalize (target.position - myTransform.position);
		float direction = Vector3.Dot (dir, transform.forward);
		if (direction > 0) 
		{ //enemy is in front of us
			if (coolDownTimer == 0) 
			{
				ph.ChangeHealth2 (lifeCount);
				coolDownTimer = 5;
			}
		}
	}

	void OnGUI ()
	{
		if (drawGUI == true)
		{
			GUI.Box(new Rect(Screen.width/5, Screen.height/2, 300, 22), "RUN!!! Or I'll kill you in 5 seconds!!");
			
		}
	}
}
