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

    #endregion

    protected bool isCdrRefreshed = true;
    protected Transform idleTransform;

    public virtual void InitWeaponInfo(WeaponData weaponData)
    {
        weaponType = weaponData.WeaponType;
        cdr = weaponData.Cdr;
        idleTransform = transform;
    }

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
