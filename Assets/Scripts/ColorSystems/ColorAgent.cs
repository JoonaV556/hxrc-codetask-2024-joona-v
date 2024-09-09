using UnityEngine;
using static ColorDataBase;

/// <summary>
/// Handles color comparisons for various 2d objects
/// </summary>
public class ColorAgent : MonoBehaviour
{
    public bool RandomizeColorAtStart = true;

    [HideInInspector]
    public ColorKey Color;

    // Randomizes color of object when Start() is called
    private void Start()
    {
        if (RandomizeColorAtStart)
            RandomizeColor();
    }

    /// <summary>
    /// Randomizes color of this color agent and its sprite
    /// </summary>
    public void RandomizeColor()
    {
        // Get random color
        ColorKey randomKey = ColorDataBase.GetRandomKey();
        SetColor(randomKey);
    }

    /// <summary>
    /// Switches to next color in colors enum
    /// </summary>
    public void NextColor()
    {
        SetColor(GetNextColor(Color));
    }

    /// <summary>
    /// Sets object color to given one
    /// </summary>
    /// <param name="key"></param>
    public void SetColor(ColorKey key)
    {
        Color colorValue = GetColorValue(key);
        // Update component color - used for comparisons between objects
        Color = key;
        // Update sprite color
        GetComponent<SpriteRenderer>().color = colorValue;
        OnColorSet(key);
    }

    protected virtual void OnColorSet(ColorKey NewColor)
    {

    }
}