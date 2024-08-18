using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public interface ITargetHolder
    {
        bool IsTarget(GameObject otherGameObject);
        void AddTarget(GameObject newTarget);
    }

    private void OnTriggerEnter(Collider other)
    {
        var targetHolder = transform.parent.GetComponent<ITargetHolder>();
        if (targetHolder != null && targetHolder.IsTarget(other.gameObject))
        {
            targetHolder.AddTarget(other.gameObject);
        }
    }
}
