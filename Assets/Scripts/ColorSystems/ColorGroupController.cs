using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ColorDataBase;

/// <summary>
/// Assignes consecutive colors to multiple child objects - used for obstacles w multiple sides
/// </summary>
public class ColorGroupController : MonoBehaviour
{
    /*
        Each time player changes color by consuming color switcher,
        this component ensures atleast one child sprite has player color assigned to it so player cannot get stuck
    */

    List<ColorAgent> Agents;

    private void OnEnable()
    {
        // Get agents in children
        Agents = GetComponentsInChildren<ColorAgent>().ToList();

        // Switch child colors when player color changes
        PlayerColorAgent.OnPlayerColorSwitched += OnPlayerColorSwitched;
    }

    private void OnDisable()
    {
        PlayerColorAgent.OnPlayerColorSwitched -= OnPlayerColorSwitched;
    }

    void OnPlayerColorSwitched(ColorKey playerKey)
    {
        int colorIndex = (int)playerKey;
        // Set colors for all childs, first child gets players color
        for (int i = 0; i < Agents.Count; i++)
        {
            ColorKey newKey = (ColorKey)colorIndex;
            Agents[i].SetColor(newKey);
            colorIndex = (int)GetNextColor(newKey);
        }
    }
}
