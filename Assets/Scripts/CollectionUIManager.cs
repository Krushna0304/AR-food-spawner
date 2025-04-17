using UnityEngine;

public class CollectionUIManager : MonoBehaviour
{
    private GameObject Collection_UI;
    private GameObject Home_UI;
    private GameObject HomeAllModels;
    private GameObject Shop_UI;
    private GameObject Shop_AllModels;

    private void Start()
    {
        Home_UI = GameObject.FindGameObjectWithTag("HomeUI");
        HomeAllModels = GameObject.FindGameObjectWithTag("HomeModel");
    }
    public void OnExitButtonClicked()
    {

        
        int count = GameObject.FindGameObjectWithTag("CollectionUI").GetComponent<CollectionManager>().idCount;

        Collection_UI = GameObject.FindGameObjectWithTag("CollectionUI");

        Collection_UI.SetActive(false);

       Home_UI.SetActive(true);
       HomeAllModels.SetActive(true);

    }

    public void OnHomeButtonClicked()
    {
        Shop_UI = GameObject.FindGameObjectWithTag("ShopUI");
        Shop_AllModels = GameObject.FindGameObjectWithTag("ShopModel");

        Shop_AllModels.SetActive(false);
        Shop_UI.SetActive(false);

        Home_UI.SetActive(true);
        HomeAllModels.SetActive(true);

        MusicOnButtonClick.Instance.ONButtonDown();
    }

}
