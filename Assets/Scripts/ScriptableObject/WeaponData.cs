using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Data/ Weapon")]
[Serializable]
public class WeaponData : CollectableDataBase
{
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private List<BaseRangeAttack> rangeAttack;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;

    public float AttackRange => attackRange;
    public int Damage => damage;
    public List<BaseRangeAttack> RangeAttack => rangeAttack;
    public WeaponType WeaponType => weaponType;
    [SerializeField] private float cdr;
    public float Cdr => cdr;
    [SerializeField] private Weapon prefab;
    public Weapon Prefab => prefab;

    public override CollectableType CollectableType { get => CollectableType.Weapon; }
    public override Sprite Sprite { get => weaponSprite; }
}
