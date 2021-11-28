using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public static bool isGameStart;

    public void Start()
    {
        isGameStart = false;
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            isGameStart = true;
            gameObject.SetActive(false);
        }
    }
}
