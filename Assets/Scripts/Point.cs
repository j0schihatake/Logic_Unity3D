﻿using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {
    public bool empty = true;
    public Vector3 pointPosition = Vector3.zero;

    void Awake() {
        pointPosition = this.gameObject.transform.position;
    }
}
