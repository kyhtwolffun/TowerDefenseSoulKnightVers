using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    #region Properties
    [Header("Properties")]
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected float cdr;

    #endregion

    protected bool isCdrRefreshed = true;

    public void InitWeaponInfo(WeaponData weaponData, bool newInstanciate = false)
    {
        //animator = weaponData.Prefab.animator;
        spriteRenderer.sprite = weaponData.Sprite;
        //transform.SetPositionAndRotation(weaponData.Prefab.transform.localPosition, weaponData.Prefab.transform.localRotation);

        weaponType = weaponData.WeaponType;
        cdr = weaponData.Cdr;
    }

    public virtual void Attack()
    {
        if (!isCdrRefreshed)
            return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
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
}

public enum WeaponType
{
    Melee,
    Range
}
