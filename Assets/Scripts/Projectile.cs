using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float lifetime = 1f;

    public void SetDestination(Vector3 destination)
    {
        var direction = destination - transform.position;
        direction = direction.normalized;
        GetComponent<Rigidbody>().velocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 2)
            Destroy(gameObject);
        if (other.gameObject.tag == "Ennemy")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(1);
            Player.singleton.PlayMockerySFX();
        }
    }
}
