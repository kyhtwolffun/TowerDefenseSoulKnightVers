using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private WeaponSystem weaponSystem;
    [SerializeField] private bool autoInteract = false;

    [Header("Events")]
    [SerializeField] private ActionParamEvent interactableEvent;
    [SerializeField] private NoParamEvent OutRangeInteractableEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractableBase interactableBase = collision.GetComponent<IInteractableBase>();
        if (interactableBase != null)
        {
            switch (interactableBase.GetInteractableType())
            {

                case InteractableType.Collectable:
                    Collectable collectable = (Collectable)interactableBase;
                    //Raise UI event
                    switch (collectable.GetCollectableType())
                    {
                        case CollectableType.Weapon:
                            if (!autoInteract && !weaponSystem.isThereFreeSlot())
                            {
                                interactableEvent.Raise(() => weaponSystem.GetWeapon((WeaponData)collectable.Interact(), collectable.OnCompleteInteract));
                            }
                            else
                            {
                                weaponSystem.GetWeapon((WeaponData)collectable.Interact(), collectable.OnCompleteInteract);
                                OutRangeInteractableEvent.Raise();
                            }
                            break;
                        case CollectableType.Tower:
                            break;
                        case CollectableType.Item:
                            break;
                        default:
                            break;
                    }
                    break;
                case InteractableType.Tower:
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Reset UI event
        IInteractableBase interactableBase = collision.GetComponent<IInteractableBase>();
        if (interactableBase != null)
        {
            OutRangeInteractableEvent.Raise();
        }
    }
}
