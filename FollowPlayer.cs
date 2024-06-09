using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject CameraHelper;
    // Update is called once per frame
    void Update()
    {
        transform.position = CameraHelper.transform.position;
    }
}
