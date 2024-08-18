using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedCell : MonoBehaviour
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
}
