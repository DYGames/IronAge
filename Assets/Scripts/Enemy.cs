using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    Transform lastTarget;
    public int Type;
    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var cave = FindObjectOfType<Cave>();
        StartCoroutine(Attack());
        while (true)
        {
            target = cave.transform;
            float d = float.MaxValue;
            foreach (var item in FindObjectsOfType<Building>())
            {
                float z = Vector3.Distance(transform.position, item.transform.position);
                if (d > z)
                {
                    d = z;
                    target = item.transform;
                }
            }
            if (lastTarget == null || lastTarget != target)
            {
                agent.SetDestination(target.position);
                lastTarget = target;
            }
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f / Type);
            if (agent.velocity.x < 0.05f && agent.velocity.y < 0.05f && agent.velocity.z < 0.05f && target != null && Vector3.Distance(transform.position, target.position) < agent.stoppingDistance + 1)
            {
                target.gameObject.GetComponent<HPController>().HP -= Type;
            }
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<WaveMng>().Enemy--;
    }
}
