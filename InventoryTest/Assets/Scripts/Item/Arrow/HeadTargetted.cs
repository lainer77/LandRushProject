using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class HeadTargetted : MonoBehaviourEx
{
    private ArrowScript _arrowScript;
    protected override void Start()
    {
        _arrowScript = transform.parent.parent.GetComponent<ArrowScript>();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag(Tags.Untagged) &&
            !collision.transform.CompareTag(Tags.RightController) &&
            !collision.transform.CompareTag(Tags.LeftController))
        {
            _arrowScript.State = ArrowScript.ArrowState.Hand;
        }
    }
}
