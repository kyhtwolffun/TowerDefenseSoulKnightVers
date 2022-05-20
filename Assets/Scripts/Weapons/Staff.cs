using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    [SerializeField] private Transform firePoint;

    public void Mage()
    {
        for (int i = 0; i < rangeAttack.Count; i++)
        {
            rangeAttack[i].Attack(firePoint, 45f, team);
        }
    }
}

