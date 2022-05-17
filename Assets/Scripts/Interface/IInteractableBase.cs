using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableBase
{
    InteractableType GetInteractableType();
}

public enum InteractableType
{
    Collectable,
    Tower
}
