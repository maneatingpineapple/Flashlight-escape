using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class FlashlightScript: MonoBehaviour
{
    public GameObject helper;
    Vector3 mousePosition;
    Vector2 direction;
    float angle;

    private void Update()
    {
        transform.position = helper.transform.position+new Vector3(0,5.2f,0);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(Vector2.right, direction) - 90;
        transform.eulerAngles = new Vector3(0, 0, angle);
        Quaternion.Euler(0, 180, 0);
    }
}