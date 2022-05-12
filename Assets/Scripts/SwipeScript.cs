using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    // added these two values, set the coll value to your collider on this object.
    bool isTouching;
    public Collider coll;

    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;
    public static int brojbacanja = 0;
    public static bool bacenaprva = false;

    [Range(0.05f, 1f)]
    public float throwForce = 0.3f;

    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && brojbacanja == 0)
        {
            if(IsTouchOverThisObject(Input.GetTouch(0)))
            {
                Debug.Log("Tach to:" + gameObject.name);
                //isTouching = true;
                //touchTimeStart = Time.time;
                //startPos = Input.GetTouch(0).position;
            }

        }
        if(isTouching && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && brojbacanja == 0)
        {
            Debug.Log("Tach to 2:" + gameObject.name);
            isTouching = false;
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.GetTouch(0).position;
            direction = startPos - endPos;
            //GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
            brojbacanja = 1;
            bacenaprva = true;
        }
    }

    bool IsTouchOverThisObject(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
        RaycastHit hit;

        // you may need to adjust the max distance paramter here based on your
        // scene size/scale.
        return coll.Raycast(ray, out hit, 1000.0f);
    }
}
