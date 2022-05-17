using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private int numberOfAvailableWeaponSlots;
    

    private List<WeaponData> weaponList = new List<WeaponData>();
    private int currentWeaponIndex;

    private Weapon currentWeapon;

    public bool isThereFreeSlot()
    {
        return numberOfAvailableWeaponSlots > 0;
    }

    public void GetWeapon(WeaponData weaponData, Action onDestroyWeaponCallback)
    {
        if (isThereFreeSlot())
        {
            FillSlot(weaponData);
        }
        else
        {
            SwitchWeapon(weaponData, true);
        }
        onDestroyWeaponCallback?.Invoke();
    }

    private void FillSlot(WeaponData weaponData)
    {
        weaponList.Add(weaponData);
        currentWeaponIndex = weaponList.IndexOf(weaponData);

        EquipWeapon();
        numberOfAvailableWeaponSlots--;
    }

    private void SwitchWeapon(WeaponData weaponData, bool isReplacing = false)
    {
        if (isReplacing)
        {
            SpawnItemSystem.instance.SpawnItem(weaponList[currentWeaponIndex], transform.parent.position);
        }

        weaponList[currentWeaponIndex] = weaponData;
        EquipWeapon();
    }

    public void SwitchWeapon()
    {
        if (weaponList.Count < 2)
            return;
        currentWeaponIndex++;
        if (currentWeaponIndex >= weaponList.Count)
            currentWeaponIndex = 0;
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        if (!currentWeapon)
        {
            currentWeapon = Instantiate(weaponList[currentWeaponIndex].Prefab, gameObject.transform);
            currentWeapon.InitWeaponInfo(weaponList[currentWeaponIndex]);
        }
        else
        {
            currentWeapon.InitWeaponInfo(weaponList[currentWeaponIndex]);
        }
    }

    public void Attack()
    {
        if (currentWeapon)
        {
            currentWeapon.Attack();
        }
    }
}
