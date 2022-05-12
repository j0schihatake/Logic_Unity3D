using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
    public Renderer rend;
    public Color myColor;

    bool isTouching;
    Collider coll;

    public static bool bacenaprva = false;

    [Range(0.05f, 1f)]
    public float throwForce = 0.3f;


    //Эта переменная будет устанавливаться в момент сортировки
    public Vector3 myPointPosition = Vector3.zero;
    public Point recuiredPosition = null;

    void Awake() {
        rend = GetComponent<Renderer>();
        myColor = rend.material.color;
        myPointPosition = this.gameObject.transform.position;
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(IsTouchOverThisObject(Input.GetTouch(0)))
            {
                OnSelected();
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

    public void OnSelected()
    {
        Debug.Log("Button touched: " + this.gameObject.name);
        if(!Base.Instance.IsMoving)
        {
            if(Base.Instance.selectedButton != null)
            {
                Base.Instance.selectedButton.GetComponent<Renderer>().material.color = Base.Instance.selectedButton.GetComponent<Buttons>().myColor;
            }
            Base.Instance.selectedButton = this;
            Base.Instance.nextPosition = myPointPosition;
            rend.material.color = Color.red;
        }
    }
}
