using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCell : MonoBehaviour, Destroyer.IDestroyListener
{

    UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Minion") 
        {
            Destroyer.Destroy(collision.gameObject);
            Destroyer.Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            Destroyer.Destroy(Player.singleton.gameObject);
            Destroyer.Destroy(gameObject);
        }
    }

    [SerializeField] GameObject deathSFXPrefab;
    public void BeforeDestroy()
    {
        Destroy(Instantiate(deathSFXPrefab, gameObject.transform.position, Quaternion.identity), 3f);
    }
}
