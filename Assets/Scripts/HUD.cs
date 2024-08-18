using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text nutriment;

    // Update is called once per frame
    void Update()
    {
        nutriment.text = "Nutriment: " + Player.singleton.stats.nutriment;
    }
}
