using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Data/ Weapon")]
[Serializable]
public class WeaponData : ScriptableObject, ICollectableData
{
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private WeaponType weaponType;
    public WeaponType WeaponType => weaponType;
    [SerializeField] private float cdr;
    public float Cdr => cdr;
    [SerializeField] private Weapon prefab;
    public Weapon Prefab => prefab;

    [Header("Gun Properties")]
    [SerializeField] private float bulletForce;
    public float BulletForce => bulletForce;

    [Header("Staff Properties")]
    [SerializeField] private int numberOfSpreadBullets;
    public int NumberOfSpreadBullets => numberOfSpreadBullets;
    [SerializeField] private float angleBetweenBullets;
    public float AngleBetweenBullets => angleBetweenBullets;

    public CollectableType CollectableType { get => CollectableType.Weapon; }
    public Sprite Sprite { get => weaponSprite; }
}
