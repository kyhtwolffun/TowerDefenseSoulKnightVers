using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangeWeaponBuilder
{
    IRangeWeaponBuilder BasicInit(RangeAttackType rangeAttackType, Transform point, Rigidbody2D rbPrefab, float bulletForce);
    IRangeAttack Build();

    //Optional
    IRangeWeaponBuilder WithNumberOfSpreadBullets(int value = 0);
    IRangeWeaponBuilder WithAngleBetweenBullets(float value = 0);
}
