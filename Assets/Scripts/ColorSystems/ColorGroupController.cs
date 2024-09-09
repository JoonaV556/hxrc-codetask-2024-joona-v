using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Assignes consecutive colors to multiple child objects - used for obstacles w multiple sides
/// </summary>
public class ColorGroupController : MonoBehaviour
{
    /*
        Each time player changes color by consuming color switcher,
        this component ensures atleast one child sprite has player color assigned to it so player cannot get stuck
    */

    public List<ColorAgent> Agents;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
}
