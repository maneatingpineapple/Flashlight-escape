using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCenterScript : MonoBehaviour
{
    SpriteRenderer sr;
    public bool able_to_unlock = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sr.material.color == Color.red)
        {
            able_to_unlock = true;
        }
    }
}
