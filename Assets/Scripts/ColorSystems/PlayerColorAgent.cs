using System;
using UnityEngine;

public class PlayerColorAgent : ColorAgent
{
    public static PlayerColorAgent instance; // singleton is required so obstacles can easily fetch player color when they are spawned, and use it to randomize child component colors
    public static Action OnCollidedWithWrongColorEvent;
    public static Action<ColorDataBase.ColorKey> OnPlayerColorSwitched;

    private void Awake()
    {
        // init singleton 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate PlayerColorAgent instance detected destroying other one");
            Destroy(instance);
            instance = this;
        }
    }

    public static ColorDataBase.ColorKey GetPlayerColor()
    {
        return instance.Color;
    }

    /// <summary>
    /// Call this function from colorized obstacles if colors dont match
    /// </summary>
    public void OnCollidedWithWrongColor()
    {
        OnCollidedWithWrongColorEvent?.Invoke();
    }

    protected override void OnColorSet(ColorDataBase.ColorKey NewColor)
    {
        OnPlayerColorSwitched?.Invoke(NewColor);
    }
}
