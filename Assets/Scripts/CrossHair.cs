using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    private Transform handTransform;
    public Vector2 offset;
    public Image crossHair;

    public Sprite defaultCrosshair;
    public Sprite redCrosshair;

    public bool isRed;

    void Start()
    {
        handTransform = transform.GetChild(0);
        Cursor.visible = false;
    }

    void Update()
    {
        handTransform.position = Input.mousePosition + new Vector3(offset.x,offset.y);
    }

    public void ChangeCrossHair()
    {
        if (isRed)
        {
            crossHair.sprite = redCrosshair;
            gameObject.transform.localScale *= 1.2f;
            return;
        }
        else
        {
            crossHair.sprite = defaultCrosshair;
            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }
}
