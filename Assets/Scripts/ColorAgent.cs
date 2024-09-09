using UnityEngine;
using static ColorDataBase;

public class ColorAgent : MonoBehaviour
{
    public ColorKey CurrentColor;

    // Randomizes color of object when Start() is called
    private void Start()
    {
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

    public void SetColor(ColorKey key)
    {
        Color colorValue = GetColorValue(key);
        // Update component color - used for comparisons between objects
        CurrentColor = key;
        // Update sprite color
        GetComponent<SpriteRenderer>().color = colorValue;
    }
}
