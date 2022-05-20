using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpreadAttack", menuName = "WeaponAttackType/ SpreadAttack")]
public class SpreadAttack : BaseRangeAttack
{
    [SerializeField] int numberOfSpreadBullets;
    public int NumberOfSpreadBullets => numberOfSpreadBullets;
    [SerializeField] float angleBetweenBullets;
    public float AngleBetweenBullets => angleBetweenBullets;

    //Only use to build new RangeAttackType
    public void Init(Transform transform, Rigidbody2D _prefab, float _bulletForce, int _numberOfSpreadBullets, float _angleBetweenBullets)
    {
        bulletPrefab = _prefab;
        bulletForce = _bulletForce;

        numberOfSpreadBullets = _numberOfSpreadBullets;
        angleBetweenBullets = _angleBetweenBullets;
    }

    public override RangeAttackType RangeAttackType => RangeAttackType.Spread;

    public override void Attack(Transform FirePoint, float errorAngle, Team ownerTeam)
    {
        float currentAngle = Mathf.Atan2(FirePoint.right.y, FirePoint.right.x) * Mathf.Rad2Deg + errorAngle;
        Rigidbody2D rb;

        for (int i = 0; i < NumberOfSpreadBullets / 2; i++)
        {
            rb = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            rb.AddForce((currentAngle + (NumberOfSpreadBullets % 2 != 0 ? AngleBetweenBullets : AngleBetweenBullets / 2) + i * AngleBetweenBullets).DegreeToVector2() * BulletForce, ForceMode2D.Impulse);
            rb.GetComponent<Bullet>().SetTeam(ownerTeam);
            rb = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            rb.AddForce((currentAngle - (NumberOfSpreadBullets % 2 != 0 ? AngleBetweenBullets : AngleBetweenBullets / 2) - i * AngleBetweenBullets).DegreeToVector2() * BulletForce, ForceMode2D.Impulse);
            rb.GetComponent<Bullet>().SetTeam(ownerTeam);
        }

        if (NumberOfSpreadBullets % 2 != 0)
        {
            rb = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            rb.AddForce((currentAngle).DegreeToVector2() * BulletForce, ForceMode2D.Impulse);
            rb.GetComponent<Bullet>().SetTeam(ownerTeam);
        }
    }
}
