using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBonus : MonoBehaviour
{
    [SerializeField] int weaponIndex = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Player.singleton.UnlockWeapon(weaponIndex);
            Destroy(gameObject);
        }
    }
}
