using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectableData
{
    CollectableType CollectableType { get; }
    [SerializeField] public Sprite Sprite { get; }
}
