using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject settingPanel;
    public void OnMouseDown()
    {
        bool checkMenuOn = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuManager>().rollUp;
        if (checkMenuOn && ItemDataManager.Instance.canTakeInput)
        {   
            transform.rotation = Quaternion.Euler(0, 50, 0);
            StartCoroutine(LoadSceneDelay());
        }
      
    }

    IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("FoodPlace");
    }
}
