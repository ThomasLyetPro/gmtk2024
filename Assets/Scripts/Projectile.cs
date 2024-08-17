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
        GetComponent<Rigidbody>().velocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.tag == "WhiteCell") Destroy(collision.gameObject);
    }
}
