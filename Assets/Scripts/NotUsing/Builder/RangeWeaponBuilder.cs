using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponBuilder : IRangeWeaponBuilder
{
    RangeAttackType _rangeAttackType;
    Transform _firePoint;
    Rigidbody2D _bulletPrefab;
    float _bulletForce;

    //optional
    float _angleBetweenBullets;
    int _numberOfSpreadBullets;

    public IRangeWeaponBuilder BasicInit(RangeAttackType rangeAttackType, Transform firePoint, Rigidbody2D rbPrefab, float bulletForce)
    {
        _rangeAttackType = rangeAttackType;
        _firePoint = firePoint;
        _bulletPrefab = rbPrefab;
        _bulletForce = bulletForce;
        return this;
    }

    public IRangeWeaponBuilder WithAngleBetweenBullets(float value = 0)
    {
        if (value != 0)
            _angleBetweenBullets = value;

        return this;
    }

    public IRangeWeaponBuilder WithNumberOfSpreadBullets(int value = 0)
    {
        if (value != 0)
            _numberOfSpreadBullets = value;

        return this;
    }

    public IRangeAttack Build()
    {
        switch (_rangeAttackType)
        {
            case RangeAttackType.Straight:
                StraightAttack straightAttack = new StraightAttack();
                straightAttack.Init(_firePoint, _bulletPrefab, _bulletForce);
                return straightAttack;
                //break;
            case RangeAttackType.Spread:
                SpreadAttack spreadAttack = new SpreadAttack();
                spreadAttack.Init(_firePoint, _bulletPrefab, _bulletForce, _numberOfSpreadBullets, _angleBetweenBullets);
                return spreadAttack;
                //break;
            default:
                return null;
                //break;
        }
    }
}
