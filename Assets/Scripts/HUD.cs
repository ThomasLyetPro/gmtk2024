using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text nutriment;
    [SerializeField] TMPro.TMP_Text health;

    // Update is called once per frame
    void Update()
    {
        nutriment.text = "Nourriture: " + Player.singleton.stats.nutriment;
        health.text = "Health: " + Player.singleton.GetComponent<Health>().currentHealth;
    }
}
