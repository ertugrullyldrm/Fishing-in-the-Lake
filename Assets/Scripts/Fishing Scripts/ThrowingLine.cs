using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for throwing the fishing line and the objects able to be caught from fishing
/// </summary>

public class ThrowingLine : MonoBehaviour
{
    public GameObject FishingLine;
    public List<Sprite> FishAvailable;
    public CatchingScript FishStatus;
	public List<int> FishCatchRates = new List<int>();

	public AudioSource FishCaughtSound;
	public GameObject FishCaughtHUD;
	public Image FishCaughtHUDImage;

	private Dictionary<int, int> FishRates = new Dictionary<int, int>();

	private Dictionary<int, Sprite> _FishDictionary = new Dictionary<int, Sprite>();

    private void Awake()
    {
        if (FishAvailable != null)
        {
            for (int i = 0; i < FishAvailable.Count; i++)
            {
                _FishDictionary.Add(i, FishAvailable[i]);
            }
        }

        if (FishingLine != null)
        {
            FishStatus = FishingLine.GetComponent<CatchingScript>();
        }

        if (FishStatus == null)
        {
            Debug.Log("Failed to load FishStatus");
        }

		for(int i = 0; i < FishAvailable.Count; i++)
		{
			FishRates.Add(i, FishCatchRates[i]);
		}

		if (FishCaughtSound != null)
		{
			FishCaughtSound = GetComponent<AudioSource>();
		}
	}

	public void CastFishingLine()
    {
        if (FishingLine != null)
        {
            FishingLine.SetActive(true);
        }
        else
        {
            Debug.Log("Fishing Line has not been added in inspector");
        }
    }

    public void ReelFishingLine()
    {
        if (FishStatus.FishCatchable == true)
        {
			if (FishCaughtSound != null)
				FishCaughtSound.Play();

            if (_FishDictionary != null)
            {
				int FishCaught = ReturnCaughtFish();

				switch (FishCaught)
				{
					case 0:
						UserStatsScript.Instance.EXP += 250;
						FishCaughtHUD.GetComponentInChildren<Text>().text = "+250 EXP!";
						break;
					case 1:
						UserStatsScript.Instance.EXP += 100;
						FishCaughtHUD.GetComponentInChildren<Text>().text = "+100 EXP!";
						break;
					case 2:
						UserStatsScript.Instance.EXP += 5000;
						FishCaughtHUD.GetComponentInChildren<Text>().text = "+5000 EXP!";
						break;
					case 3:
						UserStatsScript.Instance.LootBoxCount++;
						FishCaughtHUD.GetComponentInChildren<Text>().text = "+1 Lootbox!";
						break;
					default:
						break;
				}

				DisplayCaughtFish(FishCaught);

				if (!FishCaughtHUD.activeSelf)
					FishCaughtHUD.SetActive(true);

			}
            else
            {
				Debug.Log("No fish currently in dictionary to be caught");
            }
        }

        FishingLine.SetActive(false);
    }

	public int ReturnCaughtFish()
	{
		int AllRates = 0;

		for (int i = 0; i < FishRates.Count; i++)
		{
			AllRates += FishRates[i];
		}

		if (AllRates == 0)
			return -1;

		Random.InitState((int)System.DateTime.Now.Millisecond + System.DateTime.Now.Minute);

		int FishRate =  Random.Range(0, AllRates);

		int j = 0;

		foreach (Sprite Fish in FishAvailable)
		{
			if (FishRate < FishRates[j])
				return j;
			else
			{
				FishRate -= FishRates[j];
				j++;
			}
		}

		Debug.Log(j);
		return -1;
	}

	public void DisplayCaughtFish(int FishIndex)
	{
		FishCaughtHUDImage.sprite = _FishDictionary[FishIndex];
	}
}