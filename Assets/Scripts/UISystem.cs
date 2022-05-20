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
    private bool shootState = true;

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

    //Set UI -> Interactable and register callback
    private void SetupUIInteractable(Action action)
    {
        SetUIShootBtn(true);
        onInteractableBtnClickCallback = action;
    }

    //Set UI for Interact/Shoot Btn, if not interacting -> unregister callback
    private void SetUIShootBtn(bool isInteracting = false)
    {
        if (isInteracting)
        {
            shootAndInteractBtn.sprite = InteractBtnSprite;
            shootState = false;
        }
        else
        {
            shootAndInteractBtn.sprite = ShootBtnSprite;
            onInteractableBtnClickCallback = null;
            shootState = true;
        }
    }

    //On InteractBtn clicked
    private void OnInteractableBtnClick()
    {
        if (shootState)
            return;
        onInteractableBtnClickCallback?.Invoke();
        SetUIShootBtn();
    }

    #endregion
}
