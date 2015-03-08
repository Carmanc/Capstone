using UnityEngine;
using System.Collections;

public class AddWeapon : MonoBehaviour {

	public Transform axe;
	public Inventory inventory;    //access Inventory.cs
	bool drawGUI = false;
	
	// Use this for initialization
	void Start () 
	{
		drawGUI = false;
		//inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		GameObject inven = GameObject.FindGameObjectWithTag("Inventory");
		inventory = (Inventory)inven.GetComponent (typeof(Inventory));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (drawGUI == true && Input.GetKeyDown (KeyCode.F)) 
		{
			inventory.AddItem(0);
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
	
	void OnGUI ()
	{
		if (drawGUI == true)
		{
			GUI.Box(new Rect(Screen.width/2-51, 200, 200, 22), "Press F to pick up the axe");
			
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

}
