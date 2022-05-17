using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable<ICollectableData>
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private ICollectableData collectableData;

    public InteractableType GetInteractableType() => InteractableType.Collectable;
    public ICollectableData Interact() => collectableData;
    public void OnCompleteInteract() => DestroyCollectable();

    public CollectableType GetCollectableType()
    {
        return collectableData.CollectableType;
    }

    public void InitCollectable(ICollectableData _collectableData)
    {
        collectableData = _collectableData;
        spriteRenderer.sprite = collectableData.Sprite;
    }

    public ICollectableData ExtractCollectableData()
    {
        return collectableData;
    }

    public void DestroyCollectable()
    {
        gameObject.SetActive(false);
    }
}

public enum CollectableType
{
    Weapon,
    Tower,
    Item
}
