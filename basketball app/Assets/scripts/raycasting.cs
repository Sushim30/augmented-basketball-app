using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class raycasting : MonoBehaviour
{
    public GameObject spawn_prefab;
    GameObject spawn_object;
    bool object_spawned;

    ARRaycastManager arrayman;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        object_spawned = false;
        arrayman = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Debug.Log("touch done");
            if (arrayman.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitpose = hits[0].pose;
                if (!object_spawned)
                {
                    spawn_object = Instantiate(spawn_prefab,hitpose.position,hitpose.rotation);
                    object_spawned = true;
                }
                else
                {
                    spawn_object.transform.position = hitpose.position;
                }
            }
        }
    }
}
