using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Monobehavior script to set buttons in the Loot-Box Scene either interactable or uninteractable depending
///		on if you have any lootboxes to use or not
/// </summary>

public class LootBoxValueChecker : MonoBehaviour
{
	public GameObject LootBoxButton;

	private void OnEnable()
	{
			if(LootBoxButton != null)
			{
				if(UserStatsScript.Instance.LootBoxCount < 1)
				{
					LootBoxButton.GetComponent<Button>().interactable = false;
				}
				else
				{
					LootBoxButton.GetComponent<Button>().interactable = true;
				}
			}
		
	}
}
