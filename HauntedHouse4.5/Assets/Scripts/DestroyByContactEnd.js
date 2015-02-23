#pragma strict

private var drawGUI = false;
//private var restart;

var theEnd : Transform;

/*function Start ()
{
	restart = false;
}*/

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
	GUI.Box (Rect (Screen.width/2-200, 200, 400, 44), "Congratulations! You WIN the game!! ");
	}
}


