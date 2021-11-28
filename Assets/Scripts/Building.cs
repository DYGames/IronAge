using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    bool isPlaceable = true;
    public int Power;
    public float Speed;
    public int Defense;

    public GameObject AttackedEffect;
    public GameObject UpgradeEffect;
    public GameObject DestroyEffect;

    public GameObject Projectile;

    IEnumerator Start()
    {
        while (Projectile != null && !isPlaceable)
        {
            Transform target = null;
            float d = float.MaxValue;
            foreach (var item in FindObjectsOfType<Enemy>())
            {
                float z = Vector3.Distance(transform.position, item.transform.position);
                if (d > z)
                {
                    d = z;
                    target = item.transform;
                }
            }
            if (target != null)
            {
                GameObject o = Instantiate(Projectile);
                o.transform.position = transform.position + new Vector3(0, 1, 0);
                o.GetComponent<Projectile>().Target = target;
                o.GetComponent<Projectile>().Power = Power;
            }

            yield return new WaitForSeconds(1.0f / Speed);
        }
    }

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

    public void Upgrade()
    {
        Instantiate(UpgradeEffect).transform.position = transform.position;
    }

    public void Attacked()
    {
        Instantiate(AttackedEffect).transform.position = transform.position;
    }

    public void Destroy()
    {
        Instantiate(DestroyEffect).transform.position = transform.position;
        Destroy(gameObject);
        Stat.TowerDetroyed++;
    }
}
