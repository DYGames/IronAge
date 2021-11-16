using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMng : MonoBehaviour
{
    public float Speed = 0.1f;
    public float ScrollSpeed = 0.5f;
    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.position += Camera.main.transform.up * Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.position -= Camera.main.transform.right * Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.position -= Camera.main.transform.up * Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.position += Camera.main.transform.right * Speed;
        }
        if(Input.GetMouseButton(2))
        {
            Camera.main.transform.position -= (Camera.main.transform.up * (lastMousePosition - Input.mousePosition).y) * Speed;
            Camera.main.transform.position -= (Camera.main.transform.right * (lastMousePosition - Input.mousePosition).x) * Speed;
        }
        lastMousePosition = Input.mousePosition;
        Camera.main.orthographicSize -= Input.mouseScrollDelta.y * ScrollSpeed;
    }
}
