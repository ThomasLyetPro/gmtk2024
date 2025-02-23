using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : Weapon
{
    [SerializeField] float delayBetweenProjectile = 1f;
    float lastShot = -10f;

    bool lastPunchWasRight = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    [SerializeField] GameObject throwPunchSFX;
    [SerializeField] GameObject punchImpactSFX;
    [SerializeField] bool activateThrowPunch = true;
    public override void PrimaryAction()
    {
        if ((Time.time - lastShot) > delayBetweenProjectile)
        {
            lastShot = Time.time;
            if(activateThrowPunch)
                Destroy(Instantiate(throwPunchSFX, gameObject.transform.position, Quaternion.identity), 1.5f);
            if (lastPunchWasRight)
                animator.SetTrigger("Left");
            else
                animator.SetTrigger("Right");
            lastPunchWasRight = !lastPunchWasRight;
        }
    }

    public override void SecondaryAction()
    {
        // nothing
    }

    [SerializeField] int fistDamage = 10;
    public void OnTriggerEnterChild(Collider other)
    {
        if (other.gameObject.tag == "Ennemy")
        {
            Destroy(Instantiate(punchImpactSFX, gameObject.transform.position, Quaternion.identity), 3f);
            other.gameObject.GetComponent<Health>().TakeDamage(fistDamage);
        }
    }
}
