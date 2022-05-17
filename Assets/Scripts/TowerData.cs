using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "NewTower", menuName = "Data/ Tower")]
[Serializable]
public class TowerData : ScriptableObject, ICollectableData
{
    [SerializeField] private Sprite towerSprite;
    [SerializeField] private TowerType towerType;
    public TowerType TowerType => towerType;
    [SerializeField] private int health;
    public int Health => health;
    [SerializeField] private Tower prefab;
    public Tower Prefab => prefab;



    public CollectableType CollectableType { get => CollectableType.Tower; }
    public Sprite Sprite { get => towerSprite; }
}
