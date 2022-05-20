using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpreadRandAngleAttack", menuName = "WeaponAttackType/ SpreadRandAngleAttack")]
public class SpreadRandAngleAttack : BaseRangeAttack
{
    [SerializeField] int numberOfSpreadBullets;
    public int NumberOfSpreadBullets => numberOfSpreadBullets;
    [SerializeField] private float spreadingAngle;
    public float SpreadingAngle => spreadingAngle;

    //Only use to build new RangeAttackType
    public void Init(Transform transform, Rigidbody2D _prefab, float _bulletForce, int _numberOfSpreadBullets, float _spreadingAngle)
    {
        bulletPrefab = _prefab;
        bulletForce = _bulletForce;

        numberOfSpreadBullets = _numberOfSpreadBullets;
        spreadingAngle = _spreadingAngle;
    }

    public override RangeAttackType RangeAttackType => RangeAttackType.SpreadRandAngle;

    public override void Attack(Transform FirePoint, float errorAngle, Team ownerTeam)
    {
        float currentAngle = Mathf.Atan2(FirePoint.right.y, FirePoint.right.x) * Mathf.Rad2Deg + errorAngle;
        Rigidbody2D rb;

        for (int i = 0; i < NumberOfSpreadBullets; i++)
        {
            float randAngle = Random.Range(-spreadingAngle / 2, spreadingAngle / 2);
            rb = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            rb.AddForce((currentAngle + randAngle).DegreeToVector2() * BulletForce * Random.Range(0.8f,1.2f), ForceMode2D.Impulse);
            rb.MoveRotation(currentAngle + randAngle);
            rb.GetComponent<Bullet>().SetTeam(ownerTeam);
        }
    }
}
