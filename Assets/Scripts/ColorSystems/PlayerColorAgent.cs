using System;

public class PlayerColorAgent : ColorAgent
{
    public static Action OnCollidedWithWrongColorEvent;
    public static Action<ColorDataBase.ColorKey> OnPlayerColorSwitched;

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
