using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StraightAttack", menuName = "WeaponAttackType/ StraightAttack")]
public class StraightAttack : BaseRangeAttack
{
    //Only use to build new RangeAttackType
    public void Init(Transform transform, Rigidbody2D prefab, float bulletforce)
    {
        bulletPrefab = prefab;
        bulletForce = bulletforce;
    }

    public override RangeAttackType RangeAttackType => RangeAttackType.Straight;

    public override void Attack(Transform FirePoint, float errorAngle, Team ownerTeam)
    {
        float currentAngle = 0f;
        if (errorAngle!=0 )
            currentAngle = Mathf.Atan2(FirePoint.right.y, FirePoint.right.x) * Mathf.Rad2Deg + errorAngle;

        Rigidbody2D rb = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        rb.GetComponent<Bullet>().SetTeam(ownerTeam);
        //TODO: wierd instanciate ration
        if (errorAngle != 0)
            rb.MoveRotation(currentAngle);
        rb.AddForce((errorAngle==0 ? (Vector2)FirePoint.right : currentAngle.DegreeToVector2()) * BulletForce, ForceMode2D.Impulse);
    }
}
