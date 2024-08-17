using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : Weapon
{
    [SerializeField] VisualEffect muzzle;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float delayBetweenProjectile = 1f;
    float lastShot = -10f;

    private void Start()
    {
        muzzle.Stop();
    }

    public override void PrimaryAction()
    {
        if ((Time.time - lastShot) > delayBetweenProjectile)
        {
            lastShot = Time.time;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5f);

            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData))
            {
                //Instantiate(debugHitPointPrefab, hitData.point, Quaternion.identity);
                var projectile = Instantiate(projectilePrefab, Player.singleton.GetProjectileSpawnPoint(), Quaternion.identity);
                projectile.GetComponent<Projectile>().SetDestination(hitData.point);
                muzzle.Play();
               // StartCoroutine(StopMuzzle());
            }
        }
    }

    IEnumerator StopMuzzle()
    {
        yield return new WaitForSeconds(1f);
        muzzle.Stop();
    }


    public override void SecondaryAction()
    {
        // Do nothing
    }
}
