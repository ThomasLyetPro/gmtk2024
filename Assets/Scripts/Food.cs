using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void Start()
    {
        DOTween.Sequence().Append(transform.DORotate(new Vector3(0f, 120f, 0f), 1f).SetEase(Ease.Linear))
            .Append(transform.DORotate(new Vector3(0f, 240f, 0f), 1f).SetEase(Ease.Linear))
            .Append(transform.DORotate(new Vector3(0f, 360f, 0f), 1f).SetEase(Ease.Linear))
            .SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) // Layer is player
        { 
            Player.singleton.SpawnMinion();
            Destroy(gameObject);
        }

    }
}
