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
        Each time player changes color by consuming color switcher, or new obstacle spawns,
        this component ensures atleast one the obstacles child sprite has player color assigned to it
    */

    List<ColorAgent> Agents;

    private void OnEnable()
    {
        PlayerColorAgent.OnPlayerColorSwitched += AssignColorsToChilds;
        // Get agents in children
        Agents = GetComponentsInChildren<ColorAgent>().ToList();
    }

    private void OnDisable()
    {
        PlayerColorAgent.OnPlayerColorSwitched -= AssignColorsToChilds;
    }

    private void Start()
    {
        // Assign child colors when obstacle spawns
        AssignColorsToChilds(PlayerColorAgent.GetPlayerColor());
    }

    void AssignColorsToChilds(ColorKey playerColor)
    {
        int colorIndex = (int)playerColor;
        // Set colors for all childs, first child gets players color
        for (int i = 0; i < Agents.Count; i++)
        {
            ColorKey newKey = (ColorKey)colorIndex;
            Agents[i].SetColor(newKey);
            colorIndex = (int)GetNextColor(newKey);
        }
    }
}
