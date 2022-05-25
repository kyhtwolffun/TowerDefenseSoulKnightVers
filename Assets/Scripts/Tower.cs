using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IInteractable
{
    [Header("Components")]
    [SerializeField] private WeaponSystem weaponSystem;
    [SerializeField] private Detect detection;
    //[SerializeField] protected Transform weaponPosition;

    #region Properties
    [Header("Properties")]
    [SerializeField] protected TowerType towerType;
    public TowerType TowerType => towerType;
    [SerializeField] protected List<WeaponType> availableWeaponTypes;

    #endregion

    [SerializeField] private float cdrAttacking;


    private bool attackMode = false;


    private void Update()
    {
        if (detection.EnemyDetected && !attackMode)
        {
            attackMode = true;
            StartCoroutine(AttackLoop());
        }
        else if (!detection.EnemyDetected && attackMode)
        {
            attackMode = false;
            StopAllCoroutines();
        }
    }

    #region interface implementation
    public InteractableType GetInteractableType() => InteractableType.Tower;
    #endregion

    private IEnumerator AttackLoop()
    {
        yield return new WaitForSeconds(cdrAttacking);
        Attack();
        StartCoroutine(AttackLoop());
    }

    public void InitTowerInfo(TowerData towerData)
    {
        towerType = towerData.TowerType;
    }

    public virtual void Attack()
    {
        if (weaponSystem)
            weaponSystem.Attack();
    }

    public void PlaceWeapon(WeaponData weaponData, Action onSuccessCallback)
    {
        for (int i = 0; i < availableWeaponTypes.Count; i++)
        {
            if (weaponData.WeaponType == availableWeaponTypes[i])
            {
                weaponSystem.GetWeapon(weaponData, null);
                onSuccessCallback?.Invoke();
                return;
            }
        }
    }
}

public enum TowerType
{
    Attack,
    Shield,
    Buff
}
