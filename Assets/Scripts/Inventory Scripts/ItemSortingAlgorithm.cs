using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sorting algorithm for all the items in the inventory
/// 
/// Works by putting items into catagorized arrays then concatinating the arrays back into the original array
/// </summary>

public class ItemSortingAlgorithm : MonoBehaviour
{
	private InventoryScript _InventorySize = new InventoryScript();

	public void InitiateSort()
	{
		Sort();
	}
	
	private void Sort()
	{
		InventoryScript.ItemStruct[] ChestplateArray =	new InventoryScript.ItemStruct[_InventorySize.Inventory.Length];
		InventoryScript.ItemStruct[] GlovesArray =		new InventoryScript.ItemStruct[_InventorySize.Inventory.Length];
		InventoryScript.ItemStruct[] HelmArray =		new InventoryScript.ItemStruct[_InventorySize.Inventory.Length];
		InventoryScript.ItemStruct[] PantsArray =		new InventoryScript.ItemStruct[_InventorySize.Inventory.Length];
		InventoryScript.ItemStruct[] BootsArray =		new InventoryScript.ItemStruct[_InventorySize.Inventory.Length];
		InventoryScript.ItemStruct[] WeaponArray =		new InventoryScript.ItemStruct[_InventorySize.Inventory.Length];
		int ChestplateIndex = 0;
		int GlovesIndex = 0;
		int HelmIndex = 0;
		int PantsIndex = 0;
		int BootsIndex = 0;
		int WeaponIndex = 0;
		int TotalIndex = 0;
	
		foreach (InventoryScript.ItemStruct item in InventoryScript.InventoryInstance.Inventory)
		{
			switch (item.ItemTag)
			{
				case "Chestpiece":
					TempArrayFiller(ref ChestplateIndex	, ChestplateArray	, item);
					break;
				case "Gloves":
					TempArrayFiller(ref GlovesIndex		, GlovesArray		, item);
					break;
				case "Helm":
					TempArrayFiller(ref HelmIndex		, HelmArray			, item);
					break;
				case "Pants":
					TempArrayFiller(ref PantsIndex		, PantsArray		, item);
					break;
				case "Boots":
					TempArrayFiller(ref BootsIndex		, BootsArray		, item);
					break;
				case "Weapon":
					TempArrayFiller(ref WeaponIndex		, WeaponArray		, item);
					break;
				default:
					break;
			}
		}

		for (int i = 0; i < InventoryScript.InventoryInstance.Inventory.Length; i++)
		{
			InventoryScript.InventoryInstance.Inventory[i].CleanItem();
		}
	
		if (ChestplateIndex > 0)
			TotalIndex = AddToOldList(InventoryScript.InventoryInstance.Inventory, ChestplateArray	, TotalIndex, ChestplateIndex);
	
		if (GlovesIndex		> 0)
			TotalIndex = AddToOldList(InventoryScript.InventoryInstance.Inventory, GlovesArray		, TotalIndex, GlovesIndex);
	
		if (HelmIndex		> 0)
			TotalIndex = AddToOldList(InventoryScript.InventoryInstance.Inventory, HelmArray		, TotalIndex, HelmIndex);
	
		if (PantsIndex		> 0)
			TotalIndex = AddToOldList(InventoryScript.InventoryInstance.Inventory, PantsArray		, TotalIndex, PantsIndex);
	
		if (BootsIndex		> 0)
			TotalIndex = AddToOldList(InventoryScript.InventoryInstance.Inventory, BootsArray		, TotalIndex, BootsIndex);
	
		if (WeaponIndex		> 0)
			TotalIndex = AddToOldList(InventoryScript.InventoryInstance.Inventory, WeaponArray		, TotalIndex, WeaponIndex);
	
	}

	private void TempArrayFiller(ref int index, InventoryScript.ItemStruct[] array, InventoryScript.ItemStruct item)
	{
		array[index] = new InventoryScript.ItemStruct(item);
		++index;
	}
	
	private int AddToOldList(InventoryScript.ItemStruct[] MainScript, InventoryScript.ItemStruct[] TempScript, int Index, int ArrayLength)
	{
		for (int i = Index; i < Index + ArrayLength; i++)
		{
			MainScript[i] = new InventoryScript.ItemStruct(TempScript[i - Index]);
		}
	
		return Index + ArrayLength;
	}
}
