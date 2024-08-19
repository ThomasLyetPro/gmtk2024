using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyWeapon : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Minion" || other.gameObject.layer == 6)
        {
            var health = other.gameObject.GetComponent<Health>();
            // Collision is in children game object, wait for the collision with parent
            if (health) health.TakeDamage(damage);
        }
    }
}
