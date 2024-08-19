using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCell : MonoBehaviour, Destroyer.IDestroyListener, Detection.ITargetHolder
{
    NavMeshAgent agent;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    GameObject currentTarget = null;
    private void Update()
    {
        if (!currentTarget)
            targets.TryDequeue(out currentTarget);
        animator.SetBool("Attack", currentTarget);
        if (currentTarget)
        {
            agent.destination = currentTarget.transform.position;
        }
    }

    Queue<GameObject> targets = new Queue<GameObject>();
    public void AddTarget(GameObject newTarget)
    {
        targets.Enqueue(newTarget);
    }

    public bool IsTarget(GameObject otherGameObject)
    {
        return otherGameObject.tag == "Minion" || otherGameObject.layer == 6;
    }

    [SerializeField] GameObject deathSFXPrefab;
    public void BeforeDestroy()
    {
        Destroy(Instantiate(deathSFXPrefab, gameObject.transform.position, Quaternion.identity), 3f);
    }
}
