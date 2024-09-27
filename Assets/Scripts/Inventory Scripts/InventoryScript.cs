using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A modified inventory script that holds the information of item images, tags, and each slot the item is in
/// </summary>

public class InventoryScript : MonoBehaviour
{
    public const int NumberOfItemSlots = 36;

	public static InventoryScript InventoryInstance;

	[Serializable]
	public struct ItemStruct
	{
		public Sprite ItemSprite;
		public string ItemName;
		public string ItemTag;
		public string ItemStats;
		public int    ItemIndex;

		internal ItemStruct(ItemStruct item)
		{
			ItemSprite = item.ItemSprite;
			ItemName = item.ItemName;
			ItemTag = item.ItemTag;
			ItemStats = item.ItemStats;
			ItemIndex = item.ItemIndex;
		}

		internal void CleanItem()
		{
			ItemSprite = null;
			ItemName = null;
			ItemTag = _UnusedSlot;
			ItemStats = null;
		}
	};

	public ItemStruct[] Inventory = new ItemStruct[NumberOfItemSlots];

	public GameObject[] ItemSlots = new GameObject[NumberOfItemSlots];

	public Image	EnlargedImage;
	public Text		EnlargedName;
	public Text		EnlargedTag;
	public Text		EnlargedStats;
	public Text		EnlargedIndex;
	
	public Sprite EmptyItemSprite;

	private const string _UnusedSlot = "00";


	//Fills the Items[i] with blank templates if no items exist in that spot
	private void Awake()
	{
		InventoryInstance = this;

		DontDestroyOnLoad(this.gameObject);

		for (int i = 0; i < NumberOfItemSlots; i++)
		{
			if (Inventory[i].ItemSprite == EmptyItemSprite || Inventory[i].ItemSprite == null)
			{
				Inventory[i].ItemName = "AAA";
				Inventory[i].ItemTag = _UnusedSlot;
			}
		}
		ApplyItemChanges();
	}

	//Adds a created item into the list
	public void AddItem(Sprite sprite, string name, string tag, string stats)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
			//Determines of the slot is used or not
            if(Inventory[i].ItemTag == _UnusedSlot)
			{
				//Displays the item you have recieved
				Inventory[i].ItemSprite = sprite;
				Inventory[i].ItemName = name;
				Inventory[i].ItemTag = tag;
				Inventory[i].ItemStats = stats;
				return;
            }
        }
    }

	//Removes the item from the list
    public void RemoveItem(Text indexText)
    {
		Inventory[Convert.ToInt32(indexText.text)].CleanItem();
	}

	//Sets the item button on the inventory screen either active or inactive depending on if there is an item in
	//	it or not
	public void ApplyItemChanges()
	{
		for (int i = 0; i < NumberOfItemSlots; i++)
		{
			if (Inventory[i].ItemTag != _UnusedSlot)
			{
				ItemSlots[i].GetComponent<Image>().sprite = Inventory[i].ItemSprite;
				ItemSlots[i].GetComponent<Button>().interactable = true;
			}
			else
			{
				ItemSlots[i].GetComponent<Image>().sprite = EmptyItemSprite;
				ItemSlots[i].GetComponent<Button>().interactable = false;
			}
			Inventory[i].ItemIndex = i;
		}
	}

	//Brings the item in an enlarged image
	public void EnlargeItem(ref ItemStruct item)
	{
		EnlargedImage.sprite = item.ItemSprite;
		EnlargedName.text = item.ItemName;
		EnlargedTag.text = item.ItemTag;
		EnlargedStats.text = item.ItemStats.ToString();
		EnlargedIndex.text = item.ItemIndex.ToString();
	}

	public void EnlargeItem_Index(GameObject slot)
	{
		string gameObjectName = slot.name;
		string[] SplitName = gameObjectName.Split('_');

		int index = Convert.ToInt32(SplitName[1]);

		EnlargeItem(ref Inventory[index-1]);
	}
}
