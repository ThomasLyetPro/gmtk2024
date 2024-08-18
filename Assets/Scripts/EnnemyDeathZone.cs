using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ennemy")
            Destroyer.Destroy(other.gameObject);
    }
}
