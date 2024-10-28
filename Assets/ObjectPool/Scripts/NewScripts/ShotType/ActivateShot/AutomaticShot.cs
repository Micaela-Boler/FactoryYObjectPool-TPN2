using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticShot : PrepareShot
{
    [Header("COOLDOWN")]
    [SerializeField] private float shootCoolDown;
    private float timer;

    [Header("ANIMATOR")]
    [SerializeField] Animator animator;


    protected override void ShotCooldown() 
    {
        timer += Time.deltaTime;

        if (timer >= shootCoolDown)
        {
            if (findTarget.currentTarget != null)
            {
                timer = 0;

                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                    shotType.Shoot();
                }
                else
                {
                    shotType.Shoot();
                }
            }
        }
    }
}
