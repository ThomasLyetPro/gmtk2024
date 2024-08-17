using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) // Layer is player
        { 
            Player.singleton.SpawnMinion();
            Destroy(gameObject);
        }

    }
}
