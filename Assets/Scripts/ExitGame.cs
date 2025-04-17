using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.LegacyInputHelpers;

public class ExitGame :MonoBehaviour
{
    public GameObject carModel;
    private bool isExit;
    private void Start()
    {
        isExit = false;
    }
    public  void OnMouseDown()
    {
        if(!isExit)
        {
            isExit=true;
            transform.Translate(0, -.04f, 0);
            carModel.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 3f,ForceMode.Impulse);
           
            ItemDataManager.Instance.canTakeInput = false;
            StartCoroutine(LoadExitAnim());
        }
      
    }

    IEnumerator LoadExitAnim()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Exit");
        Application.Quit();
    }

}
