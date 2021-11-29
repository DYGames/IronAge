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

    public GameObject AttackedEffect;
    public GameObject DestroyEffect;

    public void ApplyStyleTransfer()
    {
        if (StyleTransferManager.instance)
        {
            Texture2D texture = StyleTransferManager.instance.GetEvaluation(Type).resultTexture;
            GetComponentInChildren<HeightMapMesh>().SetHeightMap(texture, texture);
        }
    }

    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var cave = FindObjectOfType<Cave>();

        ApplyStyleTransfer();

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
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f / Type);
            if (agent.velocity.x < 0.1f && agent.velocity.y < 0.1f && agent.velocity.z < 0.1f && target != null && Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance + 2)
            {
                int d = 1;
                Building bd = null;
                if ((bd = target.gameObject.GetComponent<Building>()) != null)
                {
                    d = bd.Defense;
                    target.gameObject.GetComponent<Building>().Attacked();
                }
                target.gameObject.GetComponent<HPController>().HP -= Type / d;
                Stat.DmgTaken += Type / d;
            }
        }
    }

    public void Destroy()
    {
        Instantiate(DestroyEffect).transform.position = transform.position;
        Destroy(gameObject);
        FindObjectOfType<WaveMng>().Money += Type * 100;
        FindObjectOfType<WaveMng>().Enemy--;
        Stat.EnemyKilled++;
    }
}
