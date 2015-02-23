using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

	//public PlayerLogic player;
	public Texture2D frame;
	public Rect framePosition;

	public float horizontalDistance;
	public float verticalDistance;
	public float width;
	public float height;
	public Texture2D healthBar;
	public Rect healthBarPosition;

	//public Mob target;

	public int currentHealth;
	public int playerHealth;
	public int maxHealth = 100;
	public float healthPercentage;

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
		//if (currentHealth <= 100) {
		ChangeHealth (0);
		ChangeHealth1 (0);

		//		}
		healthPercentage = (float)currentHealth / (float)maxHealth;
	}

	void OnGUI()
	{
		GUI.Box (new Rect(Screen.width * 0.5f, Screen.height * 0.1f, hbLength, 25), currentHealth + " / " + maxHealth);
		drawFrame ();
		drawBar ();
	}

	public void ChangeHealth (int health)
	{
		currentHealth += health;
		hbLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);

	}

	public void ChangeHealth1 (int health)
	{
		if (currentHealth < 100) {
						currentHealth += health;
				} else {
			currentHealth = maxHealth;
				}
		hbLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);
		
	}

	void drawFrame ()
	{
		framePosition.x = (Screen.width - framePosition.width) / 1;
		framePosition.width = Screen.width * 0.4f;
		framePosition.height = Screen.height * 0.09f;
		
		GUI.DrawTexture (framePosition, frame);
	}

	void drawBar()
	{
		healthBarPosition.x = framePosition.x + framePosition.width * horizontalDistance;
		healthBarPosition.y = framePosition.y + framePosition.height * verticalDistance;
		healthBarPosition.width = framePosition.width * width*healthPercentage;
		healthBarPosition.height = framePosition.height * height;

		GUI.DrawTexture (healthBarPosition, healthBar);
	}
}
