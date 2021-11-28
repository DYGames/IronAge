using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotate
{
    public Transform Target
    {
        get;set;
    }
    public void RotateIt();
}
