using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewAcitonParamEvent", menuName = "Event/AcitonParamEvent")]
public class ActionParamEvent : ScriptableObject
{
    private Action<Action> callback;

    public void Raise(Action action)
    {
        callback?.Invoke(action);
    }

    public void Register(Action<Action> action)
    {
        callback += action;
    }

    public void Unregister(Action<Action> action)
    {
        callback -= action;
    }
}
