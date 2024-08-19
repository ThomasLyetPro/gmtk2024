using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Minion : MonoBehaviour, Destroyer.IDestroyListener, Detection.ITargetHolder
{
    enum AIState { Nothing, FollowingPlayer, Charging, ChasingTarget };
    AIState state = AIState.FollowingPlayer;

    NavMeshAgent agent;
    Detection detectionField;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectionField = transform.GetComponentInChildren<Detection>();
        detectionField.gameObject.SetActive(false);
        agent.Warp(transform.position);
        fists = GetComponentInChildren<Fists>();
        StartCoroutine(FistFury());
    }

    Fists fists;
    private IEnumerator FistFury()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.6f);
            fists.PrimaryAction();
        }
    }

    void Update()
    {
        if (state == AIState.FollowingPlayer)
        { 
            agent.destination = Player.singleton.transform.position;
        }
        else
        {
            while (currentTarget == null && targets.Count != 0)
                targets.TryDequeue(out currentTarget);
            if (currentTarget)
            {
                state = AIState.ChasingTarget;
                agent.destination = currentTarget.transform.position;
            }
        }
    }

/*    public bool ReachedDestinationOrGaveUp()
    {
        return (!agent.pathPending) && (agent.remainingDistance <= agent.stoppingDistance) && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }*/

    [SerializeField] GameObject deathMinionSfx;
    public void BeforeDestroy()
    {
        Destroy(Instantiate(deathMinionSfx, gameObject.transform.position, Quaternion.identity), 3f);
    }

    public void ChargeTo(Vector3 destination)
    {
        if (state != AIState.FollowingPlayer) return;
        detectionField.gameObject.SetActive(true);
        state = AIState.Charging;
        agent.destination = destination;
    }

    public void RecallToPlayer()
    {
        state = AIState.FollowingPlayer;
        targets.Clear();
    }

    Queue<GameObject> targets = new Queue<GameObject>();
    GameObject currentTarget = null;
    public void AddTarget(GameObject newTarget)
    {
        targets.Enqueue(newTarget);
    }

    public bool IsTarget(GameObject otherGameObject)
    {
        return otherGameObject.tag == "Ennemy";
    }
}
