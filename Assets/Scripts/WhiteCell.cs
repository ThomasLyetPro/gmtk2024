using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCell : MonoBehaviour, Destroyer.IDestroyListener, Detection.ITargetHolder
{

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    GameObject currentTarget = null;
    private void Update()
    {
        if (!currentTarget)
            targets.TryDequeue(out currentTarget);
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

    [SerializeField] int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Minion" || collision.gameObject.layer == 6)
        {
            var health = collision.gameObject.GetComponent<Health>();
            // Collision is in children game object, wait for the collision with parent
            if (health) health.TakeDamage(damage);
        }
    }

    [SerializeField] GameObject deathSFXPrefab;
    public void BeforeDestroy()
    {
        Destroy(Instantiate(deathSFXPrefab, gameObject.transform.position, Quaternion.identity), 3f);
    }
}
