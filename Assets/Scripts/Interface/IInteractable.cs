using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    InteractableType GetInteractableType();
}

public enum InteractableType
{
    Collectable,
    Tower
}
