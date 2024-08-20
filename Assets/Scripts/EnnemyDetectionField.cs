using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDetectionField : MonoBehaviour
{
    public int tabasseurInField = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ennemy" && other.gameObject.GetComponent<WhiteCell>() != null)
            tabasseurInField++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ennemy" && other.gameObject.GetComponent<WhiteCell>() != null)
            tabasseurInField--;
    }
}
