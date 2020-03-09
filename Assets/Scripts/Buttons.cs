using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
    public Renderer rend;
    public Color myColor;

    //Эта переменная будет устанавливаться в момент сортировки
    public Vector3 myPointPosition = Vector3.zero;
    public Point recuiredPosition = null;

    void Awake() {
       rend = GetComponent<Renderer>();
        myColor = rend.material.color;
        myPointPosition = this.gameObject.transform.position;
    }

    void OnMouseDown() {
        if (!Base.Instance.IsMoving) {
            if (Base.Instance.selectedButton != null) {
                Base.Instance.selectedButton.GetComponent<Renderer>().material.color = Base.Instance.selectedButton.GetComponent<Buttons>().myColor;
            }
            Base.Instance.selectedButton = this;
            Base.Instance.nextPosition = myPointPosition; 
            rend.material.color = Color.red;
        }
    }
}
