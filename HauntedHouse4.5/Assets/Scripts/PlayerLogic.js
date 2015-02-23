#pragma strict

var Health = 200; //set player's initial health to 100
//var ThePumpkin : Transform;

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
	//Destroy (gameObject);
}

/*function AddHealth (TheHealth : int)
{
	Health += TheHealth;
	
	Destroy (gameObject);

}*/