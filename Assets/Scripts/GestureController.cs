using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GestureController : MonoBehaviour
{
/*    private ARRaycastManager xrOrigin;
    public bool isUION;
    public bool isDisableOn;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public float factor = 0.001f;
    public TextMeshProUGUI textMeshProUGUI;*/

 
   
    void Update()
    {
        
        if(true)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    if(Physics.Raycast(ray,out RaycastHit hit))
                    {
                        if(hit.collider.gameObject.CompareTag("Marker"))
                            Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
