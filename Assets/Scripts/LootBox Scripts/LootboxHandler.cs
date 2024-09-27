using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootboxHandler : MonoBehaviour
{
    public GameObject LootBox;
    public List<Sprite> LootItem;
    public Text ItemText;
	public Image ItemImage;
	public InventoryScript ItemTransferer;
	public Text LootboxText;
	public string ItemTag;
	public Text ItemStats;

	public int MinimumNumberOfStats = 0;
	public int MaximumNumberOfStats = 2;

	private int _LootIndex;
    private Dictionary<int, Sprite> _LootItemDictionary = new Dictionary<int, Sprite>();

	void Awake ()
    {
        Random.InitState((int)System.DateTime.Now.Millisecond + System.DateTime.Now.Minute);

        if (LootItem != null)
        {
            for (int i = 0; i < LootItem.Count; i++)
            {
                _LootItemDictionary.Add(i, LootItem[i]);
            }
        }

		LootboxText.text = "Lootboxes: " + UserStatsScript.Instance.LootBoxCount;
	}
	
    public void OpenLootBox()
    {
        LootBox.SetActive(false);

        _LootIndex = RandomIndex(_LootItemDictionary);

        ItemImage.sprite = _LootItemDictionary[_LootIndex];
		ItemText.text = GenerateName(_LootIndex);

        ItemText.gameObject.transform.parent.gameObject.SetActive(true);

		ItemStats.text = GenerateStats();

		ItemTransferer.AddItem(ItemImage.sprite, ItemText.text.ToString(), ItemTag, ItemStats.text.ToString());

		UserStatsScript.Instance.LootBoxCount--;
		LootboxText.text = "Lootboxes: " + UserStatsScript.Instance.LootBoxCount;

	}

    public void CloseLootBox()
    {
        LootBox.SetActive(true);

		ItemText.gameObject.transform.parent.gameObject.SetActive(false);
	}

    string GenerateName(int index)
    {
        string itemPrefix = ItemInformation.ItemPrefix[Random.Range(0, ItemInformation.ItemPrefix.Length)];
        string itemSuffix = ItemInformation.ItemSuffix[Random.Range(0, ItemInformation.ItemSuffix.Length)];

        switch (index)
        {
            case 0:
                ItemTag = "Chestpiece";
                break;
            case 1:
				ItemTag = "Gloves";
                break;
            case 2:
				ItemTag = "Helm";
                break;
            case 3:
				ItemTag = "Pants";
                break;
            case 4:
				ItemTag = "Boots";
                break;
            case 5:
				ItemTag = "Weapon";
                break;
            default:
				ItemTag = "Index larger than switch";
                break;
        }

        return (itemPrefix + " " + ItemTag + " of " + itemSuffix).ToString();
    }

	string GenerateStats()
	{
		string Stats = "";
		Dictionary<int, char> UsedStats = new Dictionary<int, char>();

		int NumberOfItemStats = Random.Range(MinimumNumberOfStats, MaximumNumberOfStats + 1);

		for (int i = 0; i < NumberOfItemStats; i++)
		{
			int stat;
			do
			{
				stat = Random.Range(0, ItemInformation.TotalItemStats);

			} while (UsedStats.ContainsKey(stat));

			UsedStats.Add(stat, '0');

			Stats += RetrieveStat(stat);
			if(i < NumberOfItemStats -1)
			{
				Stats += '\n';
				Stats += '\n';
			}
		}

		UsedStats.Clear();

		return Stats;
	}

	public int RandomIndex(Dictionary<int, Sprite> Dict)
	{
		Random.InitState((int)System.DateTime.Now.Millisecond + System.DateTime.Now.Minute);

		return Random.Range(0, Dict.Count);
	}

	private string RetrieveStat(int index)
	{
		KeyValuePair<string, int[]> RetrievedStat = new KeyValuePair<string, int[]>();

		string Stat;

		if(index > ItemInformation.SingleValueStatLength -1)
		{
			if(index > ItemInformation.TwoValueStatLength -1)
			{
				RetrievedStat =  ItemInformation.ItemTwoValueStat[index - ItemInformation.SingleValueStatLength];

				Stat = string.Format(RetrievedStat.Key, Random.Range(RetrievedStat.Value[0], RetrievedStat.Value[1]), Random.Range(RetrievedStat.Value[2], RetrievedStat.Value[3]));
			}
			else
			{
				Debug.Log("Index too big, figure out what went wrong");
				return "Error";
			};
		}
		else
		{
			RetrievedStat = ItemInformation.ItemSingleValueStat[index];

			Stat = string.Format(RetrievedStat.Key, Random.Range(RetrievedStat.Value[0], RetrievedStat.Value[1]));
		}

		return Stat;
	}

}
