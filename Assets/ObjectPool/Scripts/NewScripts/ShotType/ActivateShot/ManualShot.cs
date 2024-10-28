using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualShot : PrepareShot
{
    protected override void ShotCooldown()
    {
        if (Input.GetMouseButtonDown(0) && findTarget.currentTarget != null)
        {
            shotType.Shoot();
        }
    }
}
