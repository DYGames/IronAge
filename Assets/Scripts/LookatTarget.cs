using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatTarget : MonoBehaviour, IRotate
{
    public Transform Target { get; set; }
    void Update()
    {
        RotateIt();
    }

    public void RotateIt()
    {
        transform.LookAt(Target)
            ;
    }
}
