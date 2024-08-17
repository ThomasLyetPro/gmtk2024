using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Minion" || other.gameObject.layer == 6)
        {
            transform.parent.GetComponent<WhiteCell>().SetTarget(other.gameObject);
        }
    }
}
