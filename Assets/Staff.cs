using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    [SerializeField] private Rigidbody2D bulletPrefab;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletForce;

    [Header("SpreadStaff Properties")]
    [SerializeField] private int numberOfSpreadBullets;
    [SerializeField] private float angleBetweenBullets;

    public override void InitWeaponInfo(WeaponData weaponData)
    {
        base.InitWeaponInfo(weaponData);
        numberOfSpreadBullets = weaponData.NumberOfSpreadBullets;
        angleBetweenBullets = weaponData.AngleBetweenBullets;
    }

    float currentAngle;

    public void Mage()
    {
        currentAngle = Mathf.Atan2(firePoint.right.y, firePoint.right.x) * Mathf.Rad2Deg +45f;
        Rigidbody2D rb;

        for (int i = 0; i < numberOfSpreadBullets/2; i++)
        {
            rb = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            rb.AddForce(DegreeToVector2(currentAngle + (numberOfSpreadBullets % 2 != 0 ? angleBetweenBullets : angleBetweenBullets / 2) + i * angleBetweenBullets) * bulletForce, ForceMode2D.Impulse);
            rb = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            rb.AddForce(DegreeToVector2(currentAngle -(numberOfSpreadBullets % 2 != 0 ? angleBetweenBullets : angleBetweenBullets / 2) - i * angleBetweenBullets) * bulletForce, ForceMode2D.Impulse);
        }

        if (numberOfSpreadBullets%2 != 0)
        {
            rb = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            rb.AddForce(DegreeToVector2(currentAngle) * bulletForce, ForceMode2D.Impulse);
        }
    }

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}

