using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public override void Attack()
    {
        if (!isCdrRefreshed)
            return;
        animator.SetTrigger("Attack");
        StartCoroutine(CdrCoolingDown());
    }
}
