using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    bool isPlaceable = true;

    private void Update()
    {
        if (isPlaceable)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, 1 << 3))
                transform.position = hit.point + new Vector3(0, transform.localScale.y * 0.5f, 0);

            if (Input.GetMouseButtonDown(0))
                isPlaceable = false;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
