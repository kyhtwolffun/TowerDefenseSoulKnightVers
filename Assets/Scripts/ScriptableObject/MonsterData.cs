using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Data/ Monster")]
public class MonsterData : ScriptableObject
{
    [SerializeField] private Monster prefab;
    public Monster Prefab => prefab;
    [SerializeField] private int health;
    public int Health => health;
    [SerializeField] private float speed;
    public float Speed => speed;
}
