using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This database is required because comparing rgb values directly in game is not a good idea because of possible floating point errors.
/// When colors are compared with enums instead, they are 100% error proof.
/// </summary>
public class ColorDataBase : MonoBehaviour
{
    public enum ColorKey
    {
        Orange = 0,
        Purple = 1,
        Green = 2,
        Blue = 3
    };

    // Define static versions of custom colors (gotta match the included random color circle sprite)
    public static Color Orange = new Color(255f, 85f, 0f, 1f);
    public static Color LightBlue = new Color(89f, 255f, 240f, 1f);
    public static Color Purple = new Color(255f, 0f, 198f, 1f);
    public static Color BrightGreen = new Color(16f, 255f, 54f, 1f);

    // Inspector overriden color values
    public Color OrangeOverride;
    public Color LightBlueOverride;
    public Color PurpleOverride;
    public Color BrightGreenOverride;

    /// <summary>
    /// Dictionary of possible color keys and their respective rgba value
    /// </summary>
    /// <returns></returns>
    public static Dictionary<ColorDataBase.ColorKey, Color> GameColors = new()
    {
        { ColorKey.Orange, Orange },
        { ColorKey.Purple, Purple },
        { ColorKey.Green, BrightGreen },
        { ColorKey.Blue, LightBlue }
    };

    public static Color GetColorValue(ColorKey key)
    {
        return GameColors[key];
    }

    /// <summary>
    /// Returns random color key from possible color keys.
    /// </summary>
    public static ColorKey GetRandomKey()
    {
        return (ColorKey)Random.Range(0, 3);
    }

    // Override hardcoded colors with inspector defined ones (created with fancy picker :))
    private void Awake()
    {
        // Update colors in the database
        GameColors[ColorKey.Orange] = OrangeOverride;
        GameColors[ColorKey.Blue] = LightBlueOverride;
        GameColors[ColorKey.Purple] = PurpleOverride;
        GameColors[ColorKey.Green] = BrightGreenOverride;
    }
}
