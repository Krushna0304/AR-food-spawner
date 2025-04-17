using System.Collections;
using UnityEngine;

public class RotateMenu : MonoBehaviour
{
    private Vector2 initPos;
    private Vector2 finalPos;

    public float magnitude;
    public float thresh;
 
    void Update()
    {
        
        if(Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                initPos = touch.position;
                finalPos = Vector2.zero;
            }
            else if(touch.phase == TouchPhase.Stationary)
            {
                initPos = touch.position;
                finalPos = Vector2.zero;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                finalPos = touch.position;
            }

            else if(touch.phase == TouchPhase.Ended)
            {
                initPos = Vector2.zero;
                finalPos = Vector2.zero;
            }

            if(initPos!=Vector2.zero && finalPos!= Vector2.zero)
            {
                float force = DetectSwipe(initPos, finalPos);
                if(force !=0)
                {
                    Debug.Log(force * magnitude);
                    StopAllCoroutines();
                    StartCoroutine(Rotate(force * magnitude));
                }
            }
        }
    }

    IEnumerator Rotate(float force)
    {
        float factor;
        factor = force > 0 ? 1 : -1;

        float iteration = Mathf.Abs(force);
        while(iteration > 0)
        {
            transform.Rotate(0,iteration * factor ,0);
            iteration--;
            yield return null;
        }
    }

    float DetectSwipe(Vector2 initPos,Vector2 finalPos)
    {
        float diff = initPos.x - finalPos.x;

        if(Mathf.Abs(diff) > thresh)
        {
            return diff;
        }
        return 0;
    }
}
