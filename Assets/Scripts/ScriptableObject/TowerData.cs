using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "NewTower", menuName = "Data/ Tower")]
[Serializable]
public class TowerData : CollectableDataBase
{
    [SerializeField] private Sprite towerSprite;
    [SerializeField] private TowerType towerType;
    public TowerType TowerType => towerType;
    [SerializeField] private int health;
    public int Health => health;
    [SerializeField] private Tower prefab;
    public Tower Prefab => prefab;
    [SerializeField] private Collectable handyTower;
    public Collectable HandyTower => handyTower;



    public override CollectableType CollectableType { get => CollectableType.Tower; }
    public override Sprite Sprite { get => towerSprite; }
}
