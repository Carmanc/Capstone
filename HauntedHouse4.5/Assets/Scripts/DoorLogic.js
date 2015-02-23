#pragma strict

private var drawGUI = false;

var theDoor : Transform;
private var doorIsClosed = true;

function Update ()
{
	if (drawGUI == true && Input.GetKeyDown(KeyCode.E))
	{
		changeDoorState();	
	}
}

function OnTriggerEnter (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		drawGUI = true;
		//Debug.Log("working");
	}

}

function OnGUI ()
{
	if (drawGUI == true)
	{
	GUI.Box (Rect (Screen.width/2-51, 200, 102, 22), "Press E to open");
	}
}

function OnTriggerExit (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		drawGUI = false;
		//Debug.Log("working");
	}
}


function changeDoorState ()
{
	if (doorIsClosed == true)
	{
		theDoor.animation.CrossFade("Open");
		theDoor.audio.Play();
		doorIsClosed = false;
		yield WaitForSeconds(3);
		theDoor.animation.CrossFade("Close");
		//theDoor.audio.Play();
		doorIsClosed = true;
	}
}
