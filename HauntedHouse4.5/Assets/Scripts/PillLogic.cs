using UnityEngine;
using System.Collections;

public class PillLogic : MonoBehaviour {

	public Transform pill;
	public PlayerHealthBar ph;    //access playerHealthBar.cs
	public int recover;



	// Use this for initialization
	void Start () 
	{

		GameObject go = GameObject.FindGameObjectWithTag("Player");
		ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	
	public void addHealth ()
	{
		recover = 5;
		ph.ChangeHealth1 (recover);
	}

}
