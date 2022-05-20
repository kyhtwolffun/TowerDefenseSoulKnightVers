using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRangeAttack : ScriptableObject, IRangeAttack
{
    [SerializeField] protected Rigidbody2D bulletPrefab;
    public Rigidbody2D BulletPrefab => bulletPrefab;
    [SerializeField] protected float bulletForce;
    public float BulletForce => bulletForce;

    public virtual RangeAttackType RangeAttackType { get; }
    public virtual void Attack(Transform firePoint, float errorAngle, Team ownerTeam)
    {

    }
}
