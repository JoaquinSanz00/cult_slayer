using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    private Transform handTransform;
    public Vector2 offset;

    void Start()
    {
        handTransform = transform.GetChild(0);
        Cursor.visible = false;
    }

    void Update()
    {
        handTransform.position = Input.mousePosition + new Vector3(offset.x,offset.y);
    }
}
