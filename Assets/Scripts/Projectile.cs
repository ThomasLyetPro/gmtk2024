using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float lifetime = 1f;
    [SerializeField] GameObject impactVFX;
    [SerializeField] GameObject impactSFX;

    public void SetDestination(Vector3 destination)
    {
        var direction = destination - transform.position;
        direction = direction.normalized;
        GetComponent<Rigidbody>().velocity = direction * speed;
        Destroy(gameObject, lifetime);
    }


    [SerializeField] int damage = 25;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 2)
        {
            Destroy(Instantiate(impactVFX, gameObject.transform.position, Quaternion.identity), 2f);
            Destroy(Instantiate(impactSFX, gameObject.transform.position, Quaternion.identity), 2f);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Ennemy")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Player.singleton.PlayMockerySFX();
        }
    }
}
