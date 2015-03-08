using UnityEngine;
using System.Collections;
using System.Collections.Generic; //gives access to list

public class ItemDatabase : MonoBehaviour {

	public List<Items> items = new List<Items> ();

	void Start()
	{
		items.Add (new Items ("axe",0,"An axe made of bronze",2,Items.ItemType.Weapon));
		items.Add (new Items ("pillbottle",1,"Add health to player",2,Items.ItemType.Consumable));
		items.Add (new Items ("power potion",2,"Temporarily increase your power",2,Items.ItemType.Consumable));
	}

}