using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Collider2D collider2d;

    #region Properties
    [Header("Properties")]
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected float cdr;
    [SerializeField] protected int damage;

    [Header("Attack types")]
    public List<BaseRangeAttack> rangeAttack;

    [Header("Team")]
    [SerializeField] protected Team team;

    //[Header("TESING")]
    //[SerializeField] public SpreadAttack changeableRangeAttack;
    #endregion

    protected bool isCdrRefreshed = true;
    protected Transform idleTransform;

    public virtual void InitWeaponInfo(WeaponData weaponData, Team ownerTeam)
    {
        weaponType = weaponData.WeaponType;
        cdr = weaponData.Cdr;
        idleTransform = transform;
        damage = weaponData.Damage;
        team = ownerTeam;
        if (collider2d)
            gameObject.layer = (int)Mathf.Log(team.TeamBulletLayerMask.value, 2);

        if (weaponData.RangeAttack != null)
        {
            for (int i = 0; i < weaponData.RangeAttack.Count; i++)
            {
                switch (weaponData.RangeAttack[i].RangeAttackType)
                {
                    case RangeAttackType.Straight:
                        rangeAttack.Add((StraightAttack)weaponData.RangeAttack[i]);
                        break;
                    case RangeAttackType.Spread:
                        rangeAttack.Add((SpreadAttack)weaponData.RangeAttack[i]);
                        break;
                    case RangeAttackType.SpreadRandAngle:
                        rangeAttack.Add((SpreadRandAngleAttack)weaponData.RangeAttack[i]);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //TODO: haven't set "team" when attacking melee
    public virtual void Attack()
    {
        if (!isCdrRefreshed)
            return;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Ready"))
        {
            return;
        }
        animator.SetTrigger("Attack");
        StartCoroutine(CdrCoolingDown());
    }

    protected IEnumerator CdrCoolingDown()
    {
        isCdrRefreshed = false;
        yield return new WaitForSeconds(cdr);
        isCdrRefreshed = true;
    }

    public virtual void Disable()
    {
        spriteRenderer.enabled = false;
        if (collider2d)
            collider2d.enabled = false;
        transform.SetPositionAndRotation(idleTransform.position, idleTransform.rotation);
    }

    public virtual void Enable()
    {
        spriteRenderer.enabled = true;
        if (collider2d)
            collider2d.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider2d)
        {
            try
            {
                DamageableBase damageable = collision.transform.GetComponent<DamageableBase>();
                if (damageable)
                    damageable.TakeDamage(damage);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

[Serializable]
public enum WeaponType
{
    Melee,
    Gun,
    Staff
}
