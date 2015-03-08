using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Items> inventory = new List<Items>();
	public List<Items> slots = new List<Items> ();
	private ItemDatabase database;
	private bool showInventory = false;
	private bool showTooltip;
	private string tooltip;
	private bool draggingItem;
	private Items draggedItem;  //hold the info of the item we are dragging
	private int prevIndex;

	//public PillLogic pl;
	public PlayerHealthBar ph;

	void Start ()
	{
		for (int i = 0; i < (slotsX * slotsY); i++) 
		{
			slots.Add(new Items());
			inventory.Add (new Items());
		}
		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();
		//print (inventory.Count);
		//inventory.Add (database.items [0]);
		//inventory.Add (database.items [1]);
		//print (inventory[0].itemName);
		//inventory[0] = database.items[0];
		//inventory[1] = database.items[1];

		//RemoveItem (0);
		//print (InventoryContains (4));
	}

	void Update()
	{
		if (Input.GetButtonDown("Inventory")) 
		{
			showInventory = !showInventory;		//switch the ture and false
		}
	}

	void OnGUI()
	{
		/*if (GUI.Button (new Rect (40, 400, 100, 40), "Save"))
						SaveInventory ();
		if (GUI.Button (new Rect (40, 450, 100, 40), "Load"))
						LoadInventory ();*/
		tooltip = "";
		GUI.skin = skin;
		if(showInventory)
		{
			DrawInventory();
			if (showTooltip) 
			{
				GUI.Box (new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 80), tooltip);	
			}
		}
		/*for (int i = 0; i < inventory.Count; i++)
		{
			GUI.Label(new Rect(10, i * 20, 200, 50), inventory[i].itemName);		//x,y, width, height
		}*/
		if (draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}

	}

	void DrawInventory()
	{
		Event e = Event.current;
		int i = 0;
		for (int y = 0; y < slotsY; y++)
		{
			for (int x = 0; x < slotsX; x++)
			{
				Rect slotRect = new Rect(x * 60, y * 60, 50, 50);
				GUI.Box(slotRect, "", skin.GetStyle("Slot"));
				slots[i] = inventory[i];
				Items item = slots[i];

				if (slots[i].itemName != null)
				{
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					//event method
					if (slotRect.Contains(e.mousePosition))
					{
						CreateTooltip(slots[i]);
						showTooltip =true;
						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) //0:left mouse button
						{
							draggingItem = true;
							prevIndex = i;
							draggedItem = item;
							inventory[i] = new Items();
						}
						if (e.type == EventType.mouseUp && draggingItem)
						{
							inventory[prevIndex] = inventory[i];
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
						if (e.isMouse && e.type == EventType.MouseDown && e.button == 1)
						{
							print ("clicked" + i);
							if (item.itemType == Items.ItemType.Consumable)
							{
								UseConsumable(slots[i], i, true);
							}
						}
					}
				}
				else{
					if (slotRect.Contains(e.mousePosition))
					{
						if (e.type == EventType.mouseUp && draggingItem)
						{
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				if (tooltip == "")
				{
					showTooltip = false;
				}

				i++;
			}
		}
	}

	string CreateTooltip(Items item)
	{
		tooltip = "<color=#F781F3>" + item.itemName + "</color>\n\n" + "<color=#D0F5A9>" + item.itemDesc + "</color>";
		return tooltip;

	}


	public void AddItem(int id)
	{
		//check if we have empty slot in the inventory
		for (int  i = 0; i < inventory.Count; i++) 
		{
			if (inventory[i].itemName == null)
			{
				for (int j = 0; j < database.items.Count; j++)
				{
					//items saved based on it's id
					if (database.items[j].itemID == id)
					{
						inventory[i] = database.items[j];
					}
				}

				break;
			}
		}
	}

	void RemoveItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++) 
		{
			if (inventory[i].itemID == id)
			{
				inventory[i] = new Items();
				break;       //with this break, just remove the first item it finds not all the items with the same id.
			}
		}
	}

	bool InventoryContains(int id)
	{
		bool result = false;
		for (int i = 0; i < inventory.Count; i++) 
		{
			result = inventory[i].itemID == id;   //== checking condition
			if (result)
			{
				break;
			}
		}
		return result;
	}

	void UseConsumable(Items item, int slot, bool deleteItem)
	{
		switch (item.itemID) 
		{
		case 1:
		{
			print ("use");
			//PillLogic.FindObjectOfType
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			ph = (PlayerHealthBar)go.GetComponent (typeof(PlayerHealthBar));
			ph.ChangeHealth1(5);

			//GameObject pill = GameObject.FindGameObjectWithTag("Pill");
			//pl = (PillLogic)pill.GetComponent (typeof(PillLogic));
			//pl.addHealth();
			RemoveItem(1);
			break;
		}

		if (deleteItem)
		{
			inventory[slot] = new Items ();
		}
		}
	}
	/*
	void SaveInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
						PlayerPrefs.SetInt ("Inventory " + i, inventory[i].itemID);
	}

	void LoadInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
			inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("Inventory " + i)] : new Items();
	}*/

}





