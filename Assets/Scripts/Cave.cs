using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{
    void Start()
    {
        GetComponent<HPController>().MaxHP = 100;
    }

    private void Update()
    {
        Stat.TimeElapsed += Time.deltaTime;
    }

    public void Destroy()
    {
        FindObjectOfType<Ending>().OpenEnding();
        Destroy(gameObject);
    }
}
