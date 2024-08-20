using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    Sequence tweenAnim;
    private void Start()
    {
        tweenAnim = DOTween.Sequence();
        tweenAnim.Append(transform.DORotate(new Vector3(0f, 120f, 0f), 1f).SetEase(Ease.Linear))
            .Append(transform.DORotate(new Vector3(0f, 240f, 0f), 1f).SetEase(Ease.Linear))
            .Append(transform.DORotate(new Vector3(0f, 360f, 0f), 1f).SetEase(Ease.Linear))
            .SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) // Layer is player
        {
            Player.singleton.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
            Player.singleton.SpawnMinion();
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        DOTween.Kill(tweenAnim);
    }
}
