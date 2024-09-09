using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This database is required because comparing rgb values directly in game is not a good idea because of possible floating point errors.
/// When colors are compared with enums instead, they are 100% error proof.
/// </summary>
public static class ColorDataBase
{
    public enum ColorKey
    {
        Yellow = 0,
        Red = 1,
        Green = 2,
        Blue = 3
    }

    /// <summary>
    /// Dictionary of possible color keys and their respective rgb value
    /// </summary>
    /// <returns></returns>
    public static readonly Dictionary<ColorDataBase.ColorKey, Color> GameColors = new()
    {
        { ColorKey.Yellow, Color.yellow },
        { ColorKey.Red, Color.red },
        { ColorKey.Green, Color.green },
        { ColorKey.Blue, Color.blue }
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
}
