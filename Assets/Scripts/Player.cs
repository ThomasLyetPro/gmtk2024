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
        recallAction = allActions.FindAction("Shoot");
    }

    [SerializeField] GameObject projectileSpawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject debugHitPointPrefab;
    [SerializeField] float delayBetweenProjectile = 1f;
    float lastShot = -10f;

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

        if (recallAction.IsPressed() && (Time.time - lastShot) > delayBetweenProjectile)
        {
            lastShot = Time.time;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5f);

            RaycastHit hitData;
            if(Physics.Raycast(ray, out hitData))
            {
                //Instantiate(debugHitPointPrefab, hitData.point, Quaternion.identity);
                var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().SetDestination(hitData.point);
            }
        }
    }

    [SerializeField] GameObject minion;
    internal void SpawnMinion()
    {
        var newMinion = Instantiate(minion);
        newMinion.transform.position = gameObject.transform.position + (Vector3.forward * 2f);
    }
}
