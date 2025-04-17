using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
public class spawnObject : MonoBehaviour
{
    public XROrigin _XROrigin;
    public GameObject obj;

    private GameObject instantiatedObj;
    private List<ARRaycastHit> raycastHits = new();
    void Update()
    {
        if(Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            bool collision = _XROrigin.GetComponent<ARRaycastManager>().Raycast(touch.position,raycastHits,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

            if(collision)
            {
                if(instantiatedObj == null)
                {
                    instantiatedObj = Instantiate(obj);
                    foreach (var plane in _XROrigin.GetComponent<ARPlaneManager>().trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }

                    _XROrigin.GetComponent<ARPlaneManager>().enabled = false;
                }

                instantiatedObj.transform.position = raycastHits[0].pose.position;
            }
        }
    }
}
