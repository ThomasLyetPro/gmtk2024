using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player singleton;

    GameObject firstPositionMinion;
    List<AI_Minion> minions = new List<AI_Minion>();

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        firstPositionMinion = transform.Find("First Position Minion").gameObject;
    }

    public void Register(AI_Minion newMinion)
    {
        minions.Add(newMinion);
    }

    public void UnRegister(AI_Minion oldMinion)
    {
        minions.Remove(oldMinion);
    }
}
