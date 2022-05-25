using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDataBase : ScriptableObject, ICollectableData
{
    public virtual CollectableType CollectableType { get; }
    public virtual Sprite Sprite { get; }
}
