using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{
    void Start()
    {
        GetComponent<HPController>().MaxHP = 100;   
    }

    public void Destroy()
    {
        FindObjectOfType<Ending>().OpenEnding();
        Destroy(gameObject);
    }
}
