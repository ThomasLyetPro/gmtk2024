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


    [SerializeField] GameObject threatSFX;
    public bool IsTarget(GameObject otherGameObject)
    {
        if ( otherGameObject.tag == "Minion" || otherGameObject.layer == 6)
        {
            Destroy(Instantiate(threatSFX, gameObject.transform.position, Quaternion.identity), 3f);
            return true;
        }
        return false;
    }

    [SerializeField] GameObject deathSFXPrefab;
    public void BeforeDestroy()
    {
        SpawnNutriment();
        Destroy(Instantiate(deathSFXPrefab, gameObject.transform.position, Quaternion.identity), 3f);
    }

    [SerializeField] Transform nutrimentSpawn;
    [SerializeField] GameObject nutrimentPrefab;
    void SpawnNutriment()
    {
        for (int i =0; i < 3; i++)
        {
            var position = nutrimentSpawn.position;
            position.y = 0.7f;
            Instantiate(nutrimentPrefab, position, Quaternion.identity);
            nutrimentSpawn.RotateAround(transform.position, Vector3.up, 130);
        }

    }
}
