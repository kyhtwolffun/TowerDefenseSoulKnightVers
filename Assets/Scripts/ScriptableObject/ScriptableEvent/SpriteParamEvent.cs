using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewSpriteParamEvent", menuName = "Event/SpriteParamEvent")]
public class SpriteParamEvent : ScriptableObject
{
    private Action<Sprite> callback;

    public void Raise(Sprite sprite)
    {
        callback?.Invoke(sprite);
    }

    public void Register(Action<Sprite> action)
    {
        callback += action;
    }

    public void Unregister(Action<Sprite> action)
    {
        callback -= action;
    }
}
