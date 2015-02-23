using UnityEngine;
using System.Collections;

public class PillLogic : MonoBehaviour {

	public Transform pill;
	public PlayerHealthBar ph;    //access playerHealthBar.cs
	public int recover;

	bool drawGUI = false;

	// Use this for initialization
	void Start () 
	{
		drawGUI = false;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (drawGUI = true && Input.GetKeyDown (KeyCode.P)) 
		{
			addHealth ();
			Destroy (gameObject);
		}
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			drawGUI = true;
			Debug.Log("working");
		}
		
	}
	
	
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			drawGUI = false;
			Debug.Log("working");
		}
	}
	
	void OnGUI ()
	{
		if (drawGUI == true)
		{
			GUI.Box(new Rect(Screen.width/2-51, 200, 200, 22), "Press P to pick up the pill");
			//GUI.Box (Rect (Screen.width/2-51, 200, 102, 22), "Press P to pick up the pill");
		}
	}
	
	void addHealth ()
	{
		recover = 5;
		ph.ChangeHealth1 (recover);
	}

}
