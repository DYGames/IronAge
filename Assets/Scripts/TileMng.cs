using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMng : MonoBehaviour
{
    public int Row;
    public int Column;
    public GameObject Tile;
    IEnumerator Start()
    {
        for (int r = 0; r < Row; r++)
        {
            for (int c = 0; c < Column; c++)
            {
                GameObject o = Instantiate(Tile);
                o.transform.position = new Vector3(r * 1.1f, 0, c * 1.1f);
                o.transform.parent = transform;
                if (r == Row / 2 && c == Column / 2)
                {
                    Camera.main.transform.parent = o.transform;
                    Camera.main.transform.localPosition = Vector3.zero;
                    Camera.main.transform.parent = null;
                    Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x * 2, Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.z * 2);
                    Camera.main.orthographicSize = 20;
                }
            }
        }

        float t = 0;
        while(t <= 1)
        {
            t += Time.deltaTime / 1.5f;
            yield return null;
            Camera.main.orthographicSize = Mathf.Lerp(20, 5, t);
        }
    }

}
