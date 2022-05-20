using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private Transform firePoint;

    public override void Attack()
    {
        if (!isCdrRefreshed)
            return;
        animator.SetTrigger("Attack");
        StartCoroutine(CdrCoolingDown());
    }

    public void Shoot()
    {
        for (int i = 0; i < rangeAttack.Count; i++)
        {
            rangeAttack[i].Attack(firePoint, 0f, team);
        }
    }
}
