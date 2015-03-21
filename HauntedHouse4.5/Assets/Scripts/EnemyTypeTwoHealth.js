#pragma strict

var Health = 200; //set enemy's initial health to 200
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
	yield WaitForSeconds(2);
	Destroy (gameObject);
}
