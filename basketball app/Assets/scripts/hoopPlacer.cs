using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class HoopPlacer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab to instantiate as the hoop.")]
    private GameObject hoopPrefab;

    public GameObject HoopPrefab
    {
        get => hoopPrefab;
        set => hoopPrefab = value;
    }

    [SerializeField]
    [Tooltip("Prefab to instantiate as the ball.")]
    private GameObject ballPrefab;

    public GameObject BallPrefab
    {
        get => ballPrefab;
        set => ballPrefab = value;
    }

    public GameObject SpawnedHoop { get; private set; }
    public GameObject SpawnedBall { get; private set; }

    public static event Action OnPlacedObject;

    private ARRaycastManager raycastManager;
    private static readonly List<ARRaycastHit> Hits = new List<ARRaycastHit>();
    private bool isPlaced = false;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    public void HoopDestroyer()
    {
        if(SpawnedHoop != null)
        {
            Destroy(SpawnedHoop);
            isPlaced = false;
        }
    }

    private void Update()
    {
        if (isPlaced)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = Hits[0].pose;
                    SpawnedHoop = Instantiate(hoopPrefab, hitPose.position, Quaternion.identity);

                    isPlaced = true;

                    SpawnedBall = Instantiate(ballPrefab);
                    SpawnedBall.transform.parent = Camera.main.transform;

                    OnPlacedObject?.Invoke();
                }
            }
        }
    }
}
