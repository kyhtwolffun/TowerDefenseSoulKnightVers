using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private CollectableDataBase collectableData;

    public InteractableType GetInteractableType() => InteractableType.Collectable;
    public CollectableDataBase Interact() => collectableData;
    public void OnCompleteInteract() => DestroyCollectable();

    public CollectableType GetCollectableType()
    {
        return collectableData.CollectableType;
    }

    public void InitCollectable(CollectableDataBase _collectableData)
    {
        collectableData = _collectableData;
        spriteRenderer.sprite = collectableData.Sprite;
    }

    public CollectableDataBase ExtractCollectableData()
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
