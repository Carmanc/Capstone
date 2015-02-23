#pragma strict

var Health = 100; //set enemy's initial health to 100
var TheEnemy : Transform;

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
	//ThePumpkin.audio.Play();
	Destroy (gameObject);
}
