using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractable<T> : IInteractableBase
{
    T Interact();
    void OnCompleteInteract();
}
