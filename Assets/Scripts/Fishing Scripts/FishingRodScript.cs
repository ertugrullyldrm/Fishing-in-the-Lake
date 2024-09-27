using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///		Script for simulating a fishing line
/// </summary>

public class FishingRodScript : MonoBehaviour
{
	public Transform Bobber;
	public Transform Rod;
	public LineRenderer FishingLine;

	private void Awake()
	{
		FishingLine.SetPosition(0, Rod.transform.position);
	}

	private void Update()
	{
		if(Bobber.gameObject.activeSelf == true)
		{
			FishingLine.SetPosition(1, Bobber.transform.position);
		}
		else
		{
			FishingLine.SetPosition(1, Rod.transform.position);
		}
	}
}
