using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IInteractable
{
    [Header("Components")]
    [SerializeField] private WeaponSystem weaponSystem;
    //[SerializeField] protected Transform weaponPosition;

    #region Properties
    [Header("Properties")]
    [SerializeField] protected TowerType towerType;
    public TowerType TowerType => towerType;
    [SerializeField] private int health;

    #endregion

    [SerializeField] private float cdrAttacking;


    //TEST
    private void Start()
    {
        StartCoroutine(AttackLoop());
    }

    #region interface implementation
    public InteractableType GetInteractableType() => InteractableType.Tower;
    #endregion

    private IEnumerator AttackLoop()
    {
        Attack();
        yield return new WaitForSeconds(cdrAttacking);
        StartCoroutine(AttackLoop());
    }

    public void InitTowerInfo(TowerData towerData)
    {
        towerType = towerData.TowerType;
        health = towerData.Health;
    }

    public virtual void Attack()
    {
        if (weaponSystem)
            weaponSystem.Attack();
    }

    public void PlaceWeapon(WeaponData weaponData)
    {
        weaponSystem.GetWeapon(weaponData, null);
    }
}

public enum TowerType
{
    Attack,
    Shield,
    Buff
}
