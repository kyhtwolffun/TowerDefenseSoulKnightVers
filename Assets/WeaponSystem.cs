using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private int numberOfAvailableWeaponSlots;
    

    private List<WeaponData> weaponDataList = new List<WeaponData>();
    private List<Weapon> weaponList = new List<Weapon>();
    private int currentWeaponIndex;

    private Weapon currentWeapon;
    public bool currentWeaponExistence => currentWeapon;

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

    //Pick-up new weapon (when there is empty weapon slot)
    private void FillSlot(WeaponData weaponData)
    {
        weaponDataList.Add(weaponData);
        if (!currentWeapon)
        {
            currentWeaponIndex = weaponDataList.IndexOf(weaponData);
            weaponList.Add(EquipWeapon(true, false));
        }
        else
        {
            Weapon weapon = CreateWeapon(weaponData);
            weapon.gameObject.SetActive(false);
            weaponList.Add(weapon);
        }

        numberOfAvailableWeaponSlots--;
    }

    //Pick-up new weapon (switch with current weapon)
    private void SwitchWeapon(WeaponData weaponData, bool isReplacing = false)
    {
        if (isReplacing)
        {
            SpawnItemSystem.instance.SpawnItem(weaponDataList[currentWeaponIndex], transform.parent.position);
        }

        weaponDataList[currentWeaponIndex] = weaponData;
        weaponList[currentWeaponIndex] = EquipWeapon(true, true);
    }

    //Switch different weapon (from storage)
    public void SwitchWeapon()
    {
        if (weaponList.Count < 2)
            return;
        currentWeaponIndex++;
        if (currentWeaponIndex >= weaponList.Count)
            currentWeaponIndex = 0;
        EquipWeapon(false);
    }

    private Weapon EquipWeapon(bool newWeapon, bool removeCurrentWeapon = false)
    {
        if (newWeapon)
        {
            if (removeCurrentWeapon)
            {
                Destroy(currentWeapon.gameObject);
            }
            currentWeapon = CreateWeapon(weaponDataList[currentWeaponIndex]);
            return currentWeapon;
        }
        else
        {
            currentWeapon.gameObject.SetActive(false);
            weaponList[currentWeaponIndex].gameObject.SetActive(true);
            currentWeapon = weaponList[currentWeaponIndex];
            return null;
        }
    }

    private Weapon CreateWeapon (WeaponData weaponData)
    {
        Weapon weapon;
        weapon = Instantiate(weaponData.Prefab, gameObject.transform);
        weapon.InitWeaponInfo(weaponData);

        return weapon;
    }

    public void Attack()
    {
        if (currentWeapon)
        {
            currentWeapon.Attack();
        }
    }
}
