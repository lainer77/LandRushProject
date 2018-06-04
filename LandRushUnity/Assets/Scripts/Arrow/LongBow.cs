using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;
using Valve.VR.InteractionSystem;

public class LongBow : MonoBehaviourEx
{
    private DeviceInteraction _rightDeviceInteraction;
    public ArrowShoundPackige ArrowShoundPackige;
    public GameObject CurrentArrow { get; set; }
    public GameObject ArrowResetSocket;
    public GameObject DelegatePosition;
    public bool Nocked = false;
    private float _nockStartPos = 0.18f;
    private float _shoundPos = 0.25f;
    private float _nockMaxPos = 0.66f;

    // 100 : x = 0.48 : y
    // 0.48x = 100y;
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnCollisionEnter Arrow");
        GameObject ob = other.gameObject;
        if (ob.tag == "Arrow")
        {
            CurrentArrow = ob;
            _rightDeviceInteraction = DeviceRepository.RightDeviceInteraction;
            TriggerEventSet(true);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        Debug.Log("OnCollisionExit Arrow");
        GameObject ob = other.gameObject;
        if (ob.tag == "Arrow")
        {
            CurrentArrow = null;
            TriggerEventSet(false);
        }
    }

    private void TriggerEventSet(bool addOrRemove)
    {
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonDownEvent(StartNockArrow, addOrRemove);
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonPressEvent(ArrowUpdate, addOrRemove);
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonUpEvent(EndNockArrow, addOrRemove);
    }

    private void StartNockArrow()
    {
        Debug.Log("StartNockArrow");
        ArrowShoundPackige.NockShoundPlay();
        Nocked = true;
    }

    private void EndNockArrow()
    {
        Debug.Log("EndNockArrow");
        Nocked = false;
        Rigidbody rigidbody = CurrentArrow.GetComponent<Rigidbody>();
        ArrowShoundPackige.ShotShoundPlay();
        rigidbody.AddForce(Vector3.forward * 1000);
        TriggerEventSet(false); 
        Destroy(CurrentArrow.gameObject, 10);
    }

    private void ArrowUpdate()
    {
        if (!Nocked)
            return;
        CurrentArrow.transform.rotation = DelegatePosition.transform.rotation;
        //        DelegatePosition.transform.position = CurrentArrow.transform.position;
        //        float currnetPos = DelegatePosition.transform.localPosition.z;
        //        if (currnetPos == _shoundPos)
        //        {
        //            Debug.Log("PullShoundPlay");
        //            ArrowShoundPackige.PullShoundPlay();
        //        }
        //            if (currnetPos > _nockMaxPos)
        //            currnetPos = _nockMaxPos;
        //        else if (currnetPos < _nockStartPos)
        //            currnetPos = _nockStartPos;
        //            DelegatePosition.transform.localPosition = new Vector3(0, 0, currnetPos);
        CurrentArrow.transform.position = ArrowResetSocket.transform.position;
    }
}
