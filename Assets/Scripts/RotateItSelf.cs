using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItSelf : MonoBehaviour, IRotate
{
    public Transform Target { get; set; }

    void Update()
    {
        RotateIt();
    }

    public void RotateIt()
    {
        transform.GetChild(0).Rotate(new Vector3(1000 * Time.deltaTime, 0, 0));
    }
}
