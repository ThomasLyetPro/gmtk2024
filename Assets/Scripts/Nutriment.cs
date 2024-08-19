using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutriment : MonoBehaviour
{

    private void Start()
    {
        //var distanceToDeathZonePosition = transform.position - new Vector3(48.62f, 7.8f, -20.66f);
        //if (distanceToDeathZonePosition.sqrMagnitude < 19300) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.tag == "Minion")
        {
            Destroy(gameObject);
            Player.singleton.AddNutriment(1);
        }
    }
}
