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
        Yellow,
        Red,
        Green,
        Blue
    }

    public static readonly Dictionary<ColorDataBase.ColorKey, Color> GameColors = new()
    {
        { ColorKey.Yellow, Color.yellow },
        { ColorKey.Red, Color.red },
        { ColorKey.Green, Color.green },
        { ColorKey.Blue, Color.blue }
    };

    public static Color GetColor(ColorKey key)
    {
        return GameColors[key];
    }
}

public class ColorRandomizer : MonoBehaviour
{
    // Randomizes color of object when Start() is called

    private void Start()
    {
        /// Color.
    }
}
