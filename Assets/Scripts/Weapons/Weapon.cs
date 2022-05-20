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
        team = ownerTeam;

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
}

public enum WeaponType
{
    Melee,
    Gun,
    Staff
}
