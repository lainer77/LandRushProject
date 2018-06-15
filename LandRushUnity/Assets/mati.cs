using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mati : MonoBehaviour
{
    private PSMeshRendererUpdater mesh;
	// Use this for initialization
	void Start ()
	{
	    mesh = transform.GetChild(0).GetComponent<PSMeshRendererUpdater>();
        mesh.UpdateMeshEffect(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(10,20,0);
	}
}
