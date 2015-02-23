

var Distance;
var Target : Transform; // target which will be chasing(player)
var lookAtDistance = 15.0;
var attackRange = 10.0;
var moveSpeed = 3.0;
var Damping = 2.0;

function Update ()
{
	Distance = Vector3.Distance(Target.position, transform.position); //line calculation between player and enemy
	
	if (Distance < lookAtDistance)
	{
		renderer.material.color = Color.yellow;
		lookAt ();	
	}
	
	if (Distance > lookAtDistance)
	{
		//renderer.material.color = Color.green;
	}
	
	if (Distance < attackRange)
	{
		renderer.material.color = Color.red;
		attack ();
	}
}

function lookAt ()
{
	var rotation = Quaternion.LookRotation(Target.position - transform.position);
	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping); //slowly turn
}

function attack ()
{
	transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
}