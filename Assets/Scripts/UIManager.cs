using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;

    public bool settingON;

    private void Start()
    {
        settingON = false;
        settingPanel.SetActive(false);
    }
    public void OnSettingButtonClicked()
    {
        bool checkMenuOn = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuManager>().rollUp;
        if(checkMenuOn  && ItemDataManager.Instance.canTakeInput)
        {
            settingON = !settingON;
            settingPanel.SetActive(settingON);
        }
    }
}
