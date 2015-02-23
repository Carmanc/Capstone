#pragma strict

var TheDamage : int = 50;
var Distance : float;
var MaxDistance : float = 1.5;
var TheAxe : Transform;

function Update ()
{
	//Caculate the distance between player and enemy
	if (Input.GetButtonDown("Fire1"))
	{
		TheAxe.animation.Play("AxeAttack");
		TheAxe.audio.Play();
		var hit : RaycastHit;
		if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), hit))
		{
			Distance = hit.distance;
			if (Distance < MaxDistance)
			{
				hit.transform.SendMessage("ApplyDamage", TheDamage, SendMessageOptions.DontRequireReceiver);
				//send message to the EnemyLogic function
			}
		}
	}
}