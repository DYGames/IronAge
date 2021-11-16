using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        //NavMeshHit t;
        //NavMesh.FindClosestEdge(FindObjectOfType<Cave>().transform.position, out t, NavMesh.AllAreas);
        //GetComponent<NavMeshAgent>().SetDestination(new Vector3(t.position.x, transform.position.y, t.position.z));
        GetComponent<NavMeshAgent>().SetDestination(FindObjectOfType<Cave>().transform.position);
    }

    private void OnDestroy()
    {
        FindObjectOfType<WaveMng>().Enemy--;
    }
}
