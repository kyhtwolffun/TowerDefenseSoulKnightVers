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
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            switch (interactable.GetInteractableType())
            {

                case InteractableType.Collectable:
                    Collectable collectable = (Collectable)interactable;
                    //Raise UI event
                    switch (collectable.GetCollectableType())
                    {
                        case CollectableType.Weapon:
                            if (!autoInteract && !weaponSystem.isThereFreeSlot())
                            {
                                //Update UI + register GetWeapon callback 
                                interactableEvent.Raise(() => weaponSystem.GetWeapon((WeaponData)collectable.Interact(), collectable.OnCompleteInteract));
                            }
                            else
                            {
                                //Immediately GetWeapon (for monsters) 
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
                    Tower tower = (Tower)interactable;
                    //Raise UI event
                    switch (tower.TowerType)
                    {
                        case TowerType.Attack:
                            if (!autoInteract && weaponSystem.GetCurrentWeaponExistence)
                            {
                                //Update UI + register Place current weapon for interacting tower callback
                                interactableEvent.Raise(() =>
                                {
                                    tower.PlaceWeapon(weaponSystem.GetCurrentWeaponData);
                                    weaponSystem.DropWeapon();
                                });
                            }
                                break;
                        case TowerType.Shield:
                            break;
                        case TowerType.Buff:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Reset UI event
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            OutRangeInteractableEvent.Raise();
        }
    }
}
