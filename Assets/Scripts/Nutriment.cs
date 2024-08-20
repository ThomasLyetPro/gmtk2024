using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
            other.gameObject.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
            Player.singleton.AddNutriment(1);
        }
    }
}
