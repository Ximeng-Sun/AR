using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class TapToPlace : MonoBehaviour
{
    public ARRaycastManager raycastManager;

    public GameObject Prefab0;
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;
    
    private int selectedModelIndex = 0;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    GameObject prefabToPlace = GetPrefabByIndex(selectedModelIndex);
                    if (prefabToPlace != null)
                    {
                        GameObject obj = Instantiate(prefabToPlace, hitPose.position, Quaternion.identity);
                        obj.transform.Rotate(0, 180, 0);
                    }
                }
            }
        }
    }

    GameObject GetPrefabByIndex(int index)
    {
        switch (index)
        {
            case 0: return Prefab0;
            case 1: return Prefab1;
            case 2: return Prefab2;
            case 3: return Prefab3;
            default: return null;
        }
    }

    public void SelectModel(int index)
    {
        selectedModelIndex = index;
    }
}
