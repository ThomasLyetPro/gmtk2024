using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, Destroyer.IDestroyListener, Health.IDamageListener
{
    public class Statistic
    {
        public int nutriment = 0;
    }

    internal void AddNutriment(int number)
    {
        stats.nutriment += number;
        while(stats.nutriment >= 5)
        {
            SpawnMinion();
            stats.nutriment -= 5;
        }
    }

    public Statistic stats = new Statistic();

    public static Player singleton;

    GameObject chargeTarget;
    List<AI_Minion> minions = new List<AI_Minion>();

    InputAction chargeAction;
    InputAction recallAction;
    InputAction shootAction;
    InputAction wheelAction;
    InputAction secretAction;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        chargeTarget = transform.Find("Charge Target").gameObject;
        var allActions = GetComponent<PlayerInput>().actions;
        chargeAction = allActions.FindAction("Charge");
        recallAction = allActions.FindAction("Recall");
        shootAction = allActions.FindAction("Shoot");
        wheelAction = allActions.FindAction("Wheel");
        secretAction = allActions.FindAction("Secret");
    }

    [SerializeField] Weapon[] weapons;
    [SerializeField] bool[] isWeaponActive;
    int currentWeapon = 0;

    [SerializeField] GameObject projectileSpawnPoint;

    private void Update()
    {
        if (chargeAction.IsPressed())
        {
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
            foreach (GameObject minion in minions)
            {
                AI_Minion realMinion = minion.GetComponent<AI_Minion>();
                if (!realMinion) continue;
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                RaycastHit hitData;
                if (Physics.Raycast(ray, out hitData))
                {
                    var minionDestination = hitData.point;
                    minionDestination.y = 0;
                    realMinion.ChargeTo(minionDestination);
                }
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
            AlignToCamera();
            weapons[currentWeapon].PrimaryAction();
        }

        if (secretAction != null && secretAction.IsPressed())
        {
            Time.timeScale = (Time.timeScale + 1) % 2;
        }

            var wheelValue = wheelAction.ReadValue<Vector2>();
        if (wheelValue.y != 0)
        {

            weapons[currentWeapon].gameObject.SetActive(false);
            if (wheelValue.y > 0)
            {
                do
                {
                    currentWeapon = (currentWeapon + 1) % weapons.Length;
                } while (!isWeaponActive[currentWeapon]);
            }
            else
            {
                do
                {
                    currentWeapon--;
                    if (currentWeapon < 0) currentWeapon = weapons.Length - 1;
                } while (!isWeaponActive[currentWeapon]);
            }
            weapons[currentWeapon].gameObject.SetActive(true);
        }
    }

    private void AlignToCamera()
    {
        Vector3 newLocalEulerAngle = transform.localEulerAngles;
        newLocalEulerAngle.y = Camera.main.gameObject.transform.localEulerAngles.y;
        transform.localEulerAngles = newLocalEulerAngle;
    }

    public void UnlockWeapon(int i)
    {
        weapons[currentWeapon].gameObject.SetActive(false);
        isWeaponActive[i] = true;
        currentWeapon = i;
        weapons[currentWeapon].gameObject.SetActive(true);
    }

    public Vector3 GetProjectileSpawnPoint() { return projectileSpawnPoint.transform.position; }

    [SerializeField] GameObject minion;
    internal void SpawnMinion()
    {
        var newMinion = Instantiate(minion);
        newMinion.transform.position = gameObject.transform.position + (Vector3.forward * -2f) + Vector3.up ;
    }

    [SerializeField] GameObject deathPlayerSFXPrefab;
    [SerializeField] GameObject GameOverHUD;
    public void BeforeDestroy()
    {
        GameOverHUD.SetActive(true);
        Destroy(Instantiate(deathPlayerSFXPrefab, gameObject.transform.position, Quaternion.identity), 3f);
    }

    [SerializeField] GameObject playerMockerySFXPrefab;
    internal void PlayMockerySFX()
    {
        Destroy(Instantiate(playerMockerySFXPrefab, gameObject.transform.position, Quaternion.identity), 3f);
    }

    [SerializeField] GameObject playerHurtSFXPrefab;
    public void AfterDamageTaken()
    {
        Destroy(Instantiate(playerHurtSFXPrefab, gameObject.transform.position, Quaternion.identity), 3f);
    }
}
