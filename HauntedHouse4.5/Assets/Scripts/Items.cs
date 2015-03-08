using UnityEngine;
using System.Collections;

[System.Serializable]

public class Items {

	public string itemName;
	public int itemID;

	public string itemDesc;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemSpeed;
	public ItemType itemType;

	public enum ItemType 
	{
		Weapon,
		Consumable
	
	}

	public Items (string name, int id, string desc, int power, ItemType type)
	{
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemIcon = Resources.Load<Texture2D> ("ItemIcons/" + name);
		itemPower = power;
		itemType = type;
	}

	public Items()
	{
		
	}

}
