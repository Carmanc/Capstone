using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

	//public PlayerLogic player;
	private int gameOver=2;
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
	public int maxLife = 2;
	public int currentLife;

	// Use this for initialization
	void Start () 
	{
		//maxHealth=HealthLevelTransfer.Instance.getHealth();
		currentHealth = maxHealth;
		currentLife = maxLife;

		hbLength = Screen.width / 2;

		ChangeHealth(((HealthLevelTransfer.Instance.getHealth())-maxHealth));

	}
	
	// Update is called once per frame
	void Update () 
	{
		//if (currentHealth <= 100) {
		ChangeHealth (0);
		ChangeHealth1 (0);
		ChangeHealth2 (0);
		//		}
		healthPercentage = (float)currentHealth / (float)maxHealth;
	}

	void OnGUI()
	{
		//GUI.Box (new Rect(Screen.width * 0.5f, Screen.height * 0.1f, hbLength, 25), currentHealth + " / " + maxHealth);
		drawFrame ();
		drawBar ();
	}

	//damage to player
	public void ChangeHealth (int health)
	{
		if (currentHealth > 0) 
		{
			currentHealth += health;
			HealthLevelTransfer.Instance.setHealth(currentHealth);
			hbLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);
				
		} else 
		{
			//print ("game over");
			Application.LoadLevel(gameOver);
		}
	

	}

	//add health to player
	public void ChangeHealth1 (int health)
	{
		if (currentHealth < /*100*/maxHealth) {
						currentHealth += health;
						HealthLevelTransfer.Instance.setHealth(currentHealth);
				} else {
			currentHealth = maxHealth;
			HealthLevelTransfer.Instance.setHealth(currentHealth);
				}
		hbLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);
		
	}

	//zombie attack
	public void ChangeHealth2 (int lifeCount)
	{
		if (currentLife > 0) {
						currentLife += lifeCount;
				} else {
			//currentHealth = 0;
			Application.LoadLevel(gameOver);
				}
		//hbLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);

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
