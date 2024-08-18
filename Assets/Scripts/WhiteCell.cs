using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCell : MonoBehaviour, Destroyer.IDestroyListener
{

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    GameObject target = null;
    private void Update()
    {
        if (target)
        {
            agent.destination = target.transform.position;
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
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
