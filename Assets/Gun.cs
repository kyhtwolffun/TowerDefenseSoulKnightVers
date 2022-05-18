using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private Rigidbody2D bulletPrefab;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletForce;

    public override void InitWeaponInfo(WeaponData weaponData)
    {
        base.InitWeaponInfo(weaponData);
        bulletForce = weaponData.BulletForce;
    }

    public override void Attack()
    {
        if (!isCdrRefreshed)
            return;
        animator.SetTrigger("Attack");
        StartCoroutine(CdrCoolingDown());
    }

    public void Shoot()
    {
        Rigidbody2D rb = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
