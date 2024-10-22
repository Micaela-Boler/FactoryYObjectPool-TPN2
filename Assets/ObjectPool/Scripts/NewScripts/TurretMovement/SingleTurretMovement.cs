using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTurretMovement : TurretMovement
{
    protected override void FollowTarget()
    {
        TargetDirection();
        turreyHead.forward = targetDir;
    }
}
