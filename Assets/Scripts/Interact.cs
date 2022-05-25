using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private WeaponSystem weaponSystem;
    [SerializeField] private bool autoInteract = false;

    [Header("Events - For player only")]
    [SerializeField] private ActionParamEvent interactableEvent;
    [SerializeField] private NoParamEvent OutRangeInteractableEvent;
    [SerializeField] private SpriteParamEvent getTowerEvent;
    [SerializeField] private NoParamEvent placeTowerEvent;

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
                                //Immediately GetWeapon
                                if (weaponSystem.isThereFreeSlot())
                                    weaponSystem.GetWeapon((WeaponData)collectable.Interact(), collectable.OnCompleteInteract);
                                if (!autoInteract)
                                    OutRangeInteractableEvent.Raise();
                            }
                            break;
                        case CollectableType.Tower:
                            if (weaponSystem.CanStoreTower && !weaponSystem.IsCurrentHandyTowerExistence)
                            {
                                interactableEvent.Raise(() => weaponSystem.GetHandyTower((TowerData)collectable.Interact(), () =>
                                {
                                    collectable.OnCompleteInteract();
                                    getTowerEvent.Raise(((TowerData)(collectable.Interact())).Sprite);
                                }));
                                weaponSystem.RegisterPlaceTowerCallback(() => placeTowerEvent.Raise());
                            }
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
                            if (!autoInteract && weaponSystem.IsCurrentWeaponExistence)
                            {
                                //Update UI + register Place current weapon for interacting tower callback
                                interactableEvent.Raise(() =>
                                {
                                    WeaponData weaponData = (WeaponData)weaponSystem.GetCurrentWeaponData;
                                    if (weaponData)
                                        tower.PlaceWeapon(weaponData, () => weaponSystem.DropWeapon());
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
            if (!autoInteract)
                OutRangeInteractableEvent.Raise();
        }
    }
}
