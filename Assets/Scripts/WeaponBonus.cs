using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBonus : MonoBehaviour
{
    [SerializeField] int weaponIndex = 1;
    [SerializeField] GameObject pickUpSFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(Instantiate(pickUpSFX, gameObject.transform.position, Quaternion.identity), 3f);
            Player.singleton.UnlockWeapon(weaponIndex);
            Destroy(gameObject);
        }
    }
}
