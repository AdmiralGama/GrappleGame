using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePointController : MonoBehaviour
{
    public Material unHighlighted;
    public Material highlighted;

    /// <summary>
    /// Changes the point to its highlighted color
    /// </summary>
    public void Highlight()
    {
        this.GetComponent<Renderer>().material = highlighted;
    }

    /// <summary>
    /// Changes the point back to its normal color
    /// </summary>
    public void DeHighlight()
    {
        this.GetComponent<Renderer>().material = unHighlighted;
    }
}