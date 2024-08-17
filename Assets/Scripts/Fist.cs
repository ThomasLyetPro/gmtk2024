using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    Fists parent;
    private void Start()
    {
        parent = transform.parent.GetComponent<Fists>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parent.OnTriggerEnterChild(other);
    }
}
