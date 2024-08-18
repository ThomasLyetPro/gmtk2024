using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedCell : MonoBehaviour, Destroyer.IDestroyListener
{
    [SerializeField] Transform initialDestination;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (initialDestination) SetDestination(initialDestination);
    }

    private void SetDestination(Transform destination)
    {
        agent.destination = destination.position;
    }

    [SerializeField] GameObject nutrimentPrefab;
    public void BeforeDestroy()
    {
        var position = gameObject.transform.position;
        position.y = 0.5f;
        Instantiate(nutrimentPrefab, position, Quaternion.identity);
    }
}
