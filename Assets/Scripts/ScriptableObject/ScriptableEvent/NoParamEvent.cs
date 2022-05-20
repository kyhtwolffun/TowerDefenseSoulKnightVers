using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewNoParamEvent", menuName = "Event/NoParamEvent")]
public class NoParamEvent : ScriptableObject
{
    private Action callback;

    public void Raise()
    {
        callback?.Invoke();
    }

    public void Register(Action action)
    {
        callback += action;
    }

    public void Unregister(Action action)
    {
        callback -= action;
    }
}
