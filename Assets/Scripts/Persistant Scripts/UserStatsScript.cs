using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script holds information meant to be persisntant between scenes
/// </summary>

public class UserStatsScript : MonoBehaviour
{
	public static UserStatsScript Instance;
	private bool _Created = false;

	public int PowerRankBase = 100;
	public int PowerRankFlat = 0;
	public int PowerRankMult = 100;
	public int EXP = 0;
	public int LootBoxCount = 5;
	public float Fishing_MinimumWait = 0.5f;
	public float Fishing_MaximumWait = 4.0f;

	private void Awake()
	{

		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}

		if(!_Created)
		{
			DontDestroyOnLoad(this.gameObject);
			_Created = true;
		}
	}
}
