using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringBand : MonoBehaviour
{
    public LineRenderer Acube;
    public LineRenderer Bcube;

    public GameObject StatePoint;
	// Use this for initialization
	void Start ()
	{
	    InitBand();
	}

    private void InitBand()
    {
    }

    // Update is called once per frame
	void Update () {
	    Acube.SetPosition(1, StatePoint.transform.position);
	    Bcube.SetPosition(1, StatePoint.transform.position);
	    Acube.SetPosition(0, Acube.transform.position);
	    Bcube.SetPosition(0, Bcube.transform.position);
    }
}
