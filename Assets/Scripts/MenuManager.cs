using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] MenuElements;

    [SerializeField] private Vector3 restPos;

    [SerializeField] private GameObject UI_Collection;
    [SerializeField] private GameObject Home_Canvas;
    [SerializeField] private GameObject HomeAllModel;

    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject ShopCanvas;

    public GameObject roller;
    public GameObject Sheet;
    public int steps;
    public float speed;
    public float dist;
    [HideInInspector] public bool rollUp;
    private bool roll;

    public Button menuButton;
    public float rollTime;
    [SerializeField] private float count;

    private void Start()
    {
        roll = false;
        rollUp = true;

        ResetTextPos();
    }

    void ResetTextPos()
    {
        foreach(var ret in MenuElements)
        {
            ret.anchoredPosition = restPos;
        }
    }
    public void ONMenuButtonClicked()
    {
        StartCoroutine(MenuTimeHandler());
    }

    IEnumerator MenuTimeHandler()
    {
        bool On = GameObject.FindGameObjectWithTag("SettingUI").GetComponent<UIManager>().settingON;
        if(On)
        {
            GameObject.FindGameObjectWithTag("SettingUI").GetComponent<UIManager>().OnSettingButtonClicked();
        }

        menuButton.interactable = false;
        rollUp = !rollUp;

        if (rollUp)
        {
            StartCoroutine(TextTransition(!rollUp));
            yield return new WaitForSeconds(3f);
            roll = true;
        }
        else
        {
            roll = true;
        }
        StartCoroutine(StopRoll(rollTime));
    }

    IEnumerator StopRoll(float rollTime)
    {
        yield return new WaitForSeconds(rollTime);

        if(!rollUp)
        {
        StartCoroutine(TextTransition(!rollUp));
        }
        yield return new WaitForSeconds(.3f);
        roll = false;
        menuButton.interactable = true;
    }


    private void Update()
    {
        if(Sheet.transform.localScale.x < 0.36  && !rollUp  && roll)
        {
            roller.transform.Rotate(0, 200 * Time.deltaTime, 0);
            Vector3 rollerPos = roller.transform.position;
            rollerPos.y -= speed * Time.deltaTime;
            roller.transform.position = rollerPos;

            Vector3 size = Sheet.transform.localScale;
            size.x += .1f * Time.deltaTime;
            Sheet.transform.localScale = size;

        }

        if(Sheet.transform.localScale.x > 0  && rollUp  && roll)
        {
            roller.transform.Rotate(0, -100 * Time.deltaTime, 0);
            Vector3 rollerPos = roller.transform.position;
            rollerPos.y += speed * Time.deltaTime;
            roller.transform.position = rollerPos;

            Vector3 size = Sheet.transform.localScale;
            size.x -= .1f * Time.deltaTime;
            Sheet.transform.localScale = size;

        }
    }

    IEnumerator TextTransition(bool down)
    {
        ItemDataManager.Instance.canTakeInput = false;
        int factor;
        Vector3 targetPos = restPos;
      
        if (down)
        {
            factor = 100;
            targetPos.y -= 130;
        }
        else
        {
            factor = 0;
        }

        foreach (RectTransform ret in MenuElements)
        {
            targetPos.y -= factor;

            float i = 0;
            while(i < 10)
            {
                ret.anchoredPosition = Vector3.Lerp(ret.anchoredPosition, targetPos, i / 10);
                i += .5f;
                yield return null;
            }

            ret.anchoredPosition = targetPos;
        }
        ItemDataManager.Instance.canTakeInput = true;
    }
   
    public void ONCollectionClicked()
    {
        if(ItemDataManager.Instance.canTakeInput)
        {
            HomeAllModel.SetActive(false);
            Home_Canvas.SetActive(false);

            UI_Collection.SetActive(true);
        }
    }

    public void ONShopButtonClicked()
    {
        if (ItemDataManager.Instance.canTakeInput)
        {
            HomeAllModel.SetActive(false);
            Home_Canvas.SetActive(false);

            Shop.SetActive(true);
            ShopCanvas.SetActive(true);
        }
    }

    public void OnFeedbackButtonClicked()
    {
        if(ItemDataManager.Instance.canTakeInput)
        {
            string url = "https://surl.li/twynpf";
            Application.OpenURL(url);
        }

    }

}
