using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : Weapon
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float delayBetweenProjectile = 1f;
    float lastShot = -10f;

    bool lastPunchWasRight = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void PrimaryAction()
    {
        if ((Time.time - lastShot) > delayBetweenProjectile)
        {
            lastShot = Time.time;
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

    public void OnTriggerEnterChild(Collider other)
    {
        if (other.gameObject.tag == "WhiteCell")
        {
            Destroy(other.gameObject);
        }
    }
}
