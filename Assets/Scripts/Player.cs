using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player singleton;

    GameObject chargeTarget;
    List<AI_Minion> minions = new List<AI_Minion>();

    InputAction chargeAction;
    InputAction recallAction;
    InputAction shootAction;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        chargeTarget = transform.Find("Charge Target").gameObject;
        var allActions = GetComponent<PlayerInput>().actions;
        chargeAction = allActions.FindAction("Charge");
        recallAction = allActions.FindAction("Recall");
        shootAction = allActions.FindAction("Shoot");
    }

    [SerializeField] Weapon[] weapons;
    int currentWeapon = 0;

    [SerializeField] GameObject projectileSpawnPoint;

    private void Update()
    {
        if (chargeAction.IsPressed())
        {
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
            foreach(GameObject minion in minions)
            {
                AI_Minion realMinion = minion.GetComponent<AI_Minion>();
                if (!realMinion) continue;
                realMinion.SetTarget(chargeTarget.transform.position);
            }
        }
        else if (recallAction.IsPressed())
        {
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
            foreach (GameObject minion in minions)
            {
                AI_Minion realMinion = minion.GetComponent<AI_Minion>();
                if (!realMinion) continue;
                realMinion.RecallToPlayer();
            }
        }

        if (shootAction.IsPressed())
        {
            weapons[currentWeapon].PrimaryAction();
        }
    }

    public Vector3 GetProjectileSpawnPoint() { return projectileSpawnPoint.transform.position; }

    [SerializeField] GameObject minion;
    internal void SpawnMinion()
    {
        var newMinion = Instantiate(minion);
        newMinion.transform.position = gameObject.transform.position + (Vector3.forward * 2f);
    }
}
