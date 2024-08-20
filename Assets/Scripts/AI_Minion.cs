using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class AI_Minion : MonoBehaviour, Destroyer.IDestroyListener, Detection.ITargetHolder
{
    enum AIState { Nothing, FollowingPlayer, Charging, ChasingTarget };
    AIState state = AIState.FollowingPlayer;

    NavMeshAgent agent;
    Detection detectionField;

    [SerializeField] GameObject multiplicationSFX;

    void Start()
    {
        Destroy(Instantiate(multiplicationSFX, gameObject.transform), 3f);
        agent = GetComponent<NavMeshAgent>();
        detectionField = transform.GetComponentInChildren<Detection>();
        detectionField.gameObject.SetActive(false);
        agent.Warp(transform.position);
        fists = GetComponentInChildren<Fists>();
        StartCoroutine(FistFury());
        StartCoroutine(JumpingAnim());
    }

    Fists fists;
    bool fistFuryOn = false;
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
            fistFuryOn = currentTarget == null;
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
        agent.stoppingDistance = 1f;
    }

    public void RecallToPlayer()
    {
        state = AIState.FollowingPlayer;
        targets.Clear();
        currentTarget = null;
        agent.stoppingDistance = 2f;
        fistFuryOn = false;
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

    [SerializeField] GameObject mesh;
    IEnumerator JumpingAnim()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 5f));
            if (mesh == null || mesh.transform == null) yield break;
            mesh.transform.DOLocalMoveY(1f, 0.75f).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo);
        }
    }
}
