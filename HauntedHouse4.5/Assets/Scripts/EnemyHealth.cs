using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int currentHealth;
	public int maxHealth = 50;

	public float hbLength;

	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;
		hbLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ChangeHealth (0);
	}

	/*void OnGUI ()
	{
		GUI.Box (new Rect (10, 50, hbLength, 25), currentHealth + " / " + maxHealth);
	}*/

	public void ChangeHealth (int health)
	{
		currentHealth += health;
		hbLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);
	}
}
