using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Minion : MonoBehaviour
{
    enum AIState { Nothing, FollowingPlayer };
    AIState state = AIState.FollowingPlayer;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == AIState.FollowingPlayer)
            agent.destination = Player.singleton.transform.position;
    }

    private void OnDestroy()
    {
        Player.singleton.UnRegister(this);
    }

    public void SetTarget(Vector3 destination)
    {
        state = AIState.Nothing;
        agent.destination = destination;
    }

    public void RecallToPlayer()
    {
        state = AIState.FollowingPlayer;
    }
}
