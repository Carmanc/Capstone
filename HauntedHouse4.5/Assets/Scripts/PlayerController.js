#pragma strict

var Health = 200; //set player's initial health to 100
//var ThePumpkin : Transform;
private var gameOver = 0;
private var restart = 0;

function Start ()
	{
		gameOver = 0;
		restart = 0;
		//restartText.text = "";
		//gameOverText.text = "";
	}

	
function ApplyDamage (TheDamage : int)
{
	Health -= TheDamage;
	
	if (Health <= 0)
	{
		Dead(); //call function Dead()	
	}

}

function Dead()
{
	//Destroy (gameObject);
	Debug.Log("Player died");
	gameOver = 1;
	//Destroy (gameObject);
	restart = 1;
}

function Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

/*function AddHealth (TheHealth : int)
{
	Health += TheHealth;
	
	Destroy (gameObject);

}*/