using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [Header("ItemHolder")]
    public Transform ItemHolder;

    [Header("AUdio Clip")]
    public AudioClip buyMusic;
    public AudioClip bgMusic;

    [Header("Amount Text Holder")]
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI balanceText;

    private AudioSource m_AudioSource;
    private GameObject currentItemPrefab;
    private int currentItemIndex;
    private int balance;

    public GameObject shopCamera;

    private bool buying;
    private void Start()
    {
        m_AudioSource = GameObject.FindGameObjectWithTag("HomeManager").GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("Balance"))
        {
            PlayerPrefs.SetInt("Balance", 10000);
        }

        PlayerPrefs.SetInt("Balance", 100000);
        balance = PlayerPrefs.GetInt("Balance");
        balanceText.text = balance.ToString();

        //To set first item call OnNextButtonClicked once
        currentItemIndex = -1;
        OnNextButtonClicked();
    }

    private void OnEnable()
    {
        if (shopCamera == null)
            shopCamera = GameObject.FindGameObjectWithTag("ShopCamera");
        shopCamera.SetActive(true);
        buying = false;
    }

    private void OnDisable()
    {
        shopCamera.SetActive(false);
    }
    public void OnPreButtonClicked()
    {
        currentItemIndex--;
        if(currentItemIndex < 0)
        {
            currentItemIndex = ItemDataManager.Instance.itemsInStore.Count - 1;
        }

        if(currentItemPrefab != null)
              Destroy(currentItemPrefab);
        
        currentItemPrefab = Instantiate(ItemDataManager.Instance.itemsInStore[currentItemIndex].Model,ItemHolder);
        priceText.text = "$" + ItemDataManager.Instance.itemsInStore[currentItemIndex].Price.ToString();
    }

    public void OnNextButtonClicked()
    {
        currentItemIndex++;
        if (currentItemIndex > ItemDataManager.Instance.itemsInStore.Count - 1)
        {
            currentItemIndex = 0;
        }

        if (currentItemPrefab != null)
            Destroy(currentItemPrefab);

        currentItemPrefab = Instantiate(ItemDataManager.Instance.itemsInStore[currentItemIndex].Model, ItemHolder);
        priceText.text = "$" + ItemDataManager.Instance.itemsInStore[currentItemIndex].Price.ToString();
    }

    public void OnBuyButtonClicked(RectTransform reactTransform)
    {
        if (balance > ItemDataManager.Instance.itemsInStore[currentItemIndex].Price  && !buying)
        {
            buying = true;
            reactTransform.sizeDelta = new Vector2(450f, 450f);
            m_AudioSource.Stop();
            m_AudioSource.loop = false;
            m_AudioSource.clip = buyMusic;
            m_AudioSource.Play();


            balance -= ItemDataManager.Instance.itemsInStore[currentItemIndex].Price;
            balanceText.text = balance.ToString();

            if (ItemDataManager.Instance.itemsInStore[currentItemIndex].quantity == 0)
            {
                ItemDataManager.Instance.itemsInCollectionID.Add(currentItemIndex);
            }
            
            ItemDataManager.Instance.itemsInStore[currentItemIndex].quantity += 1;
            StartCoroutine(ResetMusic(reactTransform));
        }
    }

    IEnumerator ResetMusic(RectTransform reactTransform)
    {
        yield return new WaitForSeconds(1.5f);
        reactTransform.sizeDelta = new Vector2(500f, 500f);
        m_AudioSource.clip = bgMusic;
        m_AudioSource.Play();
        m_AudioSource.loop = true;

        buying = false;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Balance", balance);
    }

}
