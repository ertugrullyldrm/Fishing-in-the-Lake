using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the time of when the fish will be caught, where the bobber will be placed,
///		and any sound effects that may play either before or during the time that the fish is able to be caught
/// </summary>

public class CatchingScript : MonoBehaviour {

    private const float _SecondsToMinuteConversion = 60f;

	//Buffer is used in seconds, the Wait variables are minutes
    public float FishingBuffer = 10f;
	public float MinimumWait = 0.0f;
	public float MaximumWait = 0.0f;
	public float SpawnPositionY = 0.15f;
    public float SpawnPositionBorder = 0.9f;
    public GameObject WaterPlane;
    public GameObject FishHookedText;

    [HideInInspector]
    public bool FishCatchable = false;
    
    private float _TimeUntilBite;
    private Vector3 _SpawnLocation;

    private void Awake()
    {
		MinimumWait = UserStatsScript.Instance.Fishing_MinimumWait;
		MaximumWait = UserStatsScript.Instance.Fishing_MaximumWait;

		if (WaterPlane != null)
        {
            MeshRenderer MeshRenderer = WaterPlane.GetComponent<MeshRenderer>();

            if (MeshRenderer != null)
            {
                _SpawnLocation = MeshRenderer.bounds.extents;
            }
        }
	}

	//Use of OnEnable and OnDisable to trigger objects to start
    private void OnEnable()
    {
        Random.InitState((int)System.DateTime.Now.Millisecond + System.DateTime.Now.Minute);

        if (FishHookedText != null)
        {
            FishHookedText.SetActive(false);
        }

        if (WaterPlane != null)
        {
            transform.Translate(-transform.position);
            transform.Translate(FindBobberSpawnLocation());
        }
        else
        {
            Debug.Log("Position defaulted to standard position, please add the plane to [Catching Script]");
        }

        RandomTimeCreation();

		FishCatchable = false;
	}

    private void OnDisable()
    {
        if (FishHookedText != null)
        {
            FishHookedText.SetActive(false);
        }
    }

    void Update ()
    {
		//Debug purposes
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Time remaining: " + (_TimeUntilBite - Time.time));
        }

		//The player is able to catch a fish for (_TimeUntilBite) Seconds
		if (Time.time > _TimeUntilBite && Time.time - _TimeUntilBite < FishingBuffer)
        {
            if (!FishCatchable)
            {
                FishCatchable = true;
                if (FishHookedText != null)
                {
                    FishHookedText.SetActive(true);
                }

            }
        }

		//If the player misses catching the fish, they will have to wait for another fish to bite their lure
        if (Time.time > _TimeUntilBite + FishingBuffer)
        {
            if (FishHookedText != null)
            {
                FishHookedText.SetActive(false);
            }
            FishCatchable = false;
            RandomTimeCreation();
        }
	}

	//Time generation for when the fish is eligible to being caught
    void RandomTimeCreation()
    {
        float _WhenToBite = Random.Range(MinimumWait, MaximumWait) * _SecondsToMinuteConversion;
        _TimeUntilBite = Time.time + _WhenToBite;
        Debug.Log("Time until fish is active: " + _WhenToBite);
    }

	//Randomizes the bobber location
    Vector3 FindBobberSpawnLocation()
    {
        Vector3 Position = WaterPlane.transform.position;
        float RandomX = Random.Range(-_SpawnLocation.x, _SpawnLocation.x) * SpawnPositionBorder;
        float RandomZ = Random.Range(-_SpawnLocation.z, _SpawnLocation.z) * SpawnPositionBorder;
        Vector3 RandomOffset = new Vector3(RandomX, SpawnPositionY, RandomZ);

        Position += RandomOffset;
        
        return Position;
    }
}