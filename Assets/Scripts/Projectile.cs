using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Target;
    public int Power;
    public GameObject Effect;
    private IEnumerator Start()
    {
        gameObject.GetComponent<IRotate>().Target = Target;
        float angle = 30.0f;
        float target_Distance = Vector3.Distance(transform.position, Target.transform.position);
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / 9.8f);
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);
        float flightDuration = target_Distance / Vx;

        transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (9.8f * elapse_time)) * Time.deltaTime * 2, Vx * Time.deltaTime * 2);
            elapse_time += Time.deltaTime * 2;
            yield return null;
        }

        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Enemy e = collision.gameObject.GetComponent<Enemy>();
        if (e != null)
        {
            Instantiate(Effect).transform.position = transform.position;
            e.gameObject.GetComponent<HPController>().HP -= Power;
            Stat.DmgGiven += Power;
            Destroy(gameObject);
        }
    }
}
