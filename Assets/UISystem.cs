using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Sprite ShootBtnSprite;
    [SerializeField] private Sprite InteractBtnSprite;

    [Header("Components")]
    [SerializeField] private Image shootAndInteractBtn;

    [Header("Events")]
    [SerializeField] private ActionParamEvent interactableEvent;
    [SerializeField] private NoParamEvent OutRangeInteractableEvent;

    private Action onInteractableBtnClickCallback;

    private void Awake()
    {
        interactableEvent.Register(SetupUIInteractable);
        OutRangeInteractableEvent.Register(() => SetUIShootBtn());
    }

    private void Update()
    {
        //Input Interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteractableBtnClick();
        }
    }

    #region Interact/Shoot Btn

    private void SetupUIInteractable(Action action)
    {
        SetUIShootBtn(true);
        onInteractableBtnClickCallback = action;
    }

    private void SetUIShootBtn(bool isInteracting = false)
    {
        if (isInteracting)
            shootAndInteractBtn.sprite = InteractBtnSprite;
        else
            shootAndInteractBtn.sprite = ShootBtnSprite;
    }

    private void OnInteractableBtnClick()
    {
        SetUIShootBtn();
        onInteractableBtnClickCallback?.Invoke();
    }

    #endregion
}
