using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAgent : ColorAgent
{
    public static Action OnCollidedWithWrongColorEvent;

    /// <summary>
    /// Call this function from colorized obstacles if colors dont match
    /// </summary>
    public void OnCollidedWithWrongColor()
    {
        OnCollidedWithWrongColorEvent?.Invoke();
    }
}
