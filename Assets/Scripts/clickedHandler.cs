using UnityEngine;
public class ClickHandler : MonoBehaviour
{
    void OnMouseDown()
    {
       GameObject.FindGameObjectWithTag("EventManager").GetComponent<EventManager>().On3DFoodSelect(this.gameObject);
    }
}
