#pragma strict

var Health = 100; //set enemy's initial health to 100
var TheEnemyTypeOne : Transform;
var die : AnimationClip;

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
	animation.Play("die");
	yield WaitForSeconds(1);
	Destroy (gameObject);
}

