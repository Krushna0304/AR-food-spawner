using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameObject buttonObj;

    public Vector3 InitButtonPos;
    private Vector3 midButtonPos;
    public float factor;

    private Transform UIParent;
    private RectTransform buttonRectTransform;
    private GameObject tempObj;
    private int m;


    public XROrigin xrOrigin;

    [Header("3D Selection Menu")]
    public GameObject _3DMenu;

    [Header("Snaks Prefab")]
    public GameObject[] prefabs;

    [Header("Spawn Buttons")]
    public GameObject[] buttons;

    [Header("Marker")]
    public GameObject marker;

    private bool uiState;
    private GameObject instantiatedMarker;

    [Header("Tab Time")]
    public float threshTime;
    private float preTime;

    private GameObject placeObject;
    public GameObject placeButton;
    private bool markerActive;
    void Start()
    {
        markerActive = true;
        _3DMenu.SetActive(false);
        placeButton.SetActive(false);
        preTime = Time.time;
        uiState = true;

        m = 1;
        LoadItemsInfo();


      
    }

    private void Update()
    {

        if(Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (IsDubbleTab())
            {
                _3DUIEnabled();
            }
        }
       
        if(markerActive)
        {
            UpdatePose();
        }
    }


    bool IsDubbleTab()
    {
        float currentTime = Time.time;

        if(currentTime - preTime < threshTime)
        {
            preTime = 0;
            return true;
        }
        preTime = currentTime;
        return false;
    }
    void UpdatePose()
    {
        List<ARRaycastHit> hit = new();
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        bool collision = xrOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hit,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
        if (collision)
        {
           if(instantiatedMarker == null)
            {
                instantiatedMarker = Instantiate(marker);
                
            }
           instantiatedMarker.transform.position = hit[0].pose.position;
        }


    }

    private void LoadItemsInfo()
    {
        UIParent = GameObject.FindGameObjectWithTag("UI").transform;
        midButtonPos = InitButtonPos;
        foreach (var itemIndex in ItemDataManager.Instance.itemsForAR)
        {
            //Create Button

            tempObj = Instantiate(buttonObj, UIParent);
            tempObj.transform.SetParent(UIParent);
            buttonRectTransform = tempObj.GetComponent<RectTransform>();
            buttonRectTransform.anchoredPosition = midButtonPos;

            //Adding Image to Button
            tempObj.GetComponent<Image>().sprite = ItemDataManager.Instance.itemsInStore[itemIndex].Image;

            //Adding function to button
            Button button = tempObj.GetComponent<Button>();
            button.onClick.AddListener(() => OnSpawnPointClicked(ItemDataManager.Instance.itemsInStore[itemIndex].Model));

            midButtonPos.x += m * factor;
            m *= -1;

            factor += 10;
        }
    }

    public void OnSpawnPointClicked(GameObject obj)
    {
        Instantiate(obj, instantiatedMarker.transform.position,Quaternion.identity);
    }


    public void OnUIButtonClicked()
    {
        uiState =  !uiState;

        markerActive = uiState;
       foreach(var button in buttons)
        {
            if(button.name != "UIToggle")
            button.SetActive(uiState);
        }
       instantiatedMarker.SetActive(uiState);

        foreach (var plane in xrOrigin.GetComponent<ARPlaneManager>().trackables)
        {
            plane.gameObject.SetActive(uiState);
        }

       // placeButton.SetActive(!uiState);
         xrOrigin.GetComponent<ARPlaneManager>().enabled = uiState;
    }

    public void OnRefreshButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void _3DUIEnabled()
    {
        uiState = true;
        _3DMenu.SetActive(true);
        OnUIButtonClicked();
        if(placeButton.activeInHierarchy)
        {
        placeButton.SetActive(false);
        }
        xrOrigin.GetComponent<ARPlaneManager>().enabled = true;
    }

    public void On3DFoodSelect(GameObject obj)
    {
        _3DMenu.SetActive(false);
        placeButton.SetActive(true);
        instantiatedMarker.SetActive(true);

        foreach (GameObject t in prefabs)
        {
            if(t.name == obj.name)
            {
                placeObject = t;
            }
        }

        markerActive = true;

    }
    public void OnPlaceButtonClicked()
    {
        if(placeObject!= null)
        Instantiate(placeObject, instantiatedMarker.transform.position, Quaternion.identity);
    }
}
