using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleByGun : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            GetComponent<StudioEventEmitter>().Play();
            Destroy(gameObject);
        }
    }
}
