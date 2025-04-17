using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public Vector3 restPos;
    public float dist;
    public GameObject itemContainer;
    public TextMeshProUGUI itemCountText;

    private RectTransform itemReactTransform;
    private Transform UIParent;
    [HideInInspector] public int idCount;
    void Start()
    {
     //  LoadCollectionUI();
     //  itemCountText.text = 0.ToString();
    }


    private void OnEnable()
    {
        LoadCollectionUI();
        itemCountText.text = 0.ToString();
        ItemDataManager.Instance.itemsForAR.Clear();
        idCount = 0;
    }

    public void LoadCollectionUI()
    {
        UIParent = GameObject.FindGameObjectWithTag("CollectionUI").GetComponent<Transform>();      
        Vector3 pos = restPos;
        foreach (var itemID in ItemDataManager.Instance.itemsInCollectionID)
        {

            if (itemID < ItemDataManager.Instance.itemsInStore.Count)
            {
                GameObject temp = Instantiate(itemContainer, UIParent);
                itemReactTransform =temp.GetComponent<RectTransform>();
                itemReactTransform.anchoredPosition = pos;
                pos.y -= dist;


                Debug.Log(temp.transform.GetChild(1).name);
                temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ItemDataManager.Instance.itemsInStore[itemID].ItemName;
                temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text += ItemDataManager.Instance.itemsInStore[itemID].quantity.ToString();
                temp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text += ItemDataManager.Instance.itemsInStore[itemID].Price.ToString();
                temp.transform.GetChild(4).name = itemID.ToString();

                Button getButton = temp.transform.GetChild(4).GetComponent<Button>();
                getButton.onClick.AddListener(() => OnGetButtonClicked(temp.transform.GetChild(4)));
                getButton.onClick.AddListener(() => GameObject.FindGameObjectWithTag("HomeManager").GetComponent<MusicOnButtonClick>().ONButtonDown());

                temp.transform.GetChild(3).GetComponent<Image>().sprite = ItemDataManager.Instance.itemsInStore[itemID].Image;

            }
            
        }
    }

    public void OnGetButtonClicked(Transform button)
    {
        string name = button.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        int id = int.Parse(button.name);

        if (name == "GET"  && idCount < 3)
        {
            idCount++;
            button.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LEAVE";
            
            if (!ItemDataManager.Instance.itemsForAR.Contains(id))
                ItemDataManager.Instance.itemsForAR.Add(id);
        }
        
        if(name == "LEAVE")
        {
            idCount--;
            button.GetChild(0).GetComponent<TextMeshProUGUI>().text = "GET";
     
            if (ItemDataManager.Instance.itemsForAR.Contains(id))
                ItemDataManager.Instance.itemsForAR.Remove(id);

        }

        itemCountText.text = idCount.ToString();
    }

}
