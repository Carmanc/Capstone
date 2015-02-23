var Distance;
var Target : Transform; // target which will be chasing(player)
var lookAtDistance = 10.0;
var attackRange = 1.5;
var moveSpeed = 3.0;
var Damping = 2.0;
var attackRepeaTime = 1; 
private var attackTime : float;
//private var attackNow = true;
var chaseRange = 8.0;
var controller: CharacterController;
var gravity : float = 20.0;
private var MoveDirection : Vector3 = Vector3.zero;
var TheDamage = 40;
var TheAnimator : Animator;

function Awake ()
{
	Target = GameObject.FindWithTag("Player").transform;
}

function Start ()
{
	attackTime = Time.time;
}

function Update ()
{
	Distance = Vector3.Distance(Target.position, transform.position); //line calculation between player and enemy
	
	if (Distance < lookAtDistance)
	{
		
		lookAt ();	
	}
	
	if (Distance > lookAtDistance)
	{
		TheAnimator.SetBool("WalkIsOn", false);
		//renderer.material.color = Color.green;
	}
	
	if (Distance < attackRange)
	{
		attack ();
	}
	
	else if (Distance < chaseRange)
	{
		
		chase ();
	}
}

function lookAt ()
{
	//renderer.material.color = Color.yellow;
	var rotation = Quaternion.LookRotation(Target.position - transform.position);
	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping); //slowly turn
}

function chase ()
{
	renderer.material.color = Color.red;
	//Target.animation("Walk");
	TheAnimator.SetBool("WalkIsOn", true);
	moveDirection = transform.forward;
	moveDirection *= moveSpeed;
	moveDirection.y -= gravity * Time.deltaTime;
	controller.Move(moveDirection * Time.deltaTime);	
}

function attack ()
{
	if (Time.time > attackTime)
	{
		Target.SendMessage("ApplyDamage", TheDamage, SendMessageOptions.DontRequireReceiver);
		Debug.Log("Attack");
		
		attackTime = Time.time * attackRepeaTime;
	}
	
}

function ApplyDamage ()
{
	chaseRange += 3.0;
	moveSpeed += 2.0;
	lookAtDistance += 4.0;
}