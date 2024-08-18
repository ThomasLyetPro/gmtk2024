using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutriment : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(gameObject);
            Player.singleton.AddNutriment(1);
        }
    }
}
