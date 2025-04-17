using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

[Serializable]
public class ItemData {
    [SerializeField] int itemID;
    [SerializeField] string name;
    [SerializeField] int price;
    [SerializeField] public int quantity;
    [SerializeField] Sprite image;
    [SerializeField] GameObject model;

    // Public Getters
    public int ItemID => itemID;
    public string ItemName => name;
    public int Price => price;
    public Sprite Image => image;
    public GameObject Model => model;
}

public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager Instance { get; private set; }

     public List<ItemData> itemsInStore = new ();
     public List<int> itemsInCollectionID = new();
     public List<int> itemsForAR = new();

    [HideInInspector] public bool canTakeInput; 
    private void Awake()
    {

        if(Instance == null)
        {
             canTakeInput = true;
             Instance = this;
             DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(Instance);
        }

    }
}
