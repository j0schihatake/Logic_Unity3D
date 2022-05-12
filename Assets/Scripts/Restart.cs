using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{

    Collider coll;

    void Awake()
    {
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(IsTouchOverThisObject(Input.GetTouch(0)))
            {
                Base.Instance.restart();
            }

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
