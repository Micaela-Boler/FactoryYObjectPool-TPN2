using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrepareShot : MonoBehaviour
{
    [Header("OTHER SCRIPTS")]
    public ShotType shotType;
    public  FindTarget findTarget;


    protected void Update()
    {
        ShotCooldown();
    }

    protected abstract void ShotCooldown(); 
}
