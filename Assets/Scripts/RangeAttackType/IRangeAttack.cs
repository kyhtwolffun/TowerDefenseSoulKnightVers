using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangeAttack
{
    RangeAttackType RangeAttackType { get; }
    Rigidbody2D BulletPrefab { get; }
    float BulletForce { get; }

    void Attack(Transform firePoint, float errorAngle, Team ownerTeam);
}

[System.Serializable]
public enum RangeAttackType
{
    Null,
    Straight,
    Spread,
    SpreadRandAngle
}
