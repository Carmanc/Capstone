#pragma strict

var TheHealth : int = 50;
var ThePill : Transform;

function OnTriggerEnter (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		
		ThePill.SendMessage("AddHealth", TheHealth, SendMessageOptions.DontRequireReceiver);
	}

}