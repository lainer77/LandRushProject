using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class StringBand : MonoBehaviourEx
{
    public LineRenderer Acube;
    public LineRenderer Bcube;

    public GameObject StatePoint;
	// Use this for initialization
    // Update is called once per frame
    /// <inheritdoc />
    protected override void Update () {
	    Acube.SetPosition(1, StatePoint.transform.position);
	    Bcube.SetPosition(1, StatePoint.transform.position);
	    Acube.SetPosition(0, Acube.transform.position);
	    Bcube.SetPosition(0, Bcube.transform.position);
    }
}
