using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityScriptHelper;
using Valve.VR.InteractionSystem;

public class LongBow : MonoBehaviourEx
{
    private DeviceInteraction _rightDeviceInteraction;
    public ArrowShoundPackige ArrowShoundPackige;
    public GameObject CurrentArrow { get; set; }
    public GameObject DelegatePosition;
    public bool Nocked = false;
    private float _nockStartPos = 0.2f;
    private float _shoundPos = 0.25f;
    private float _nockMaxPos = 0.7f;
    private float _acceleration = 5f;
    private Animator _animator;

    private AnimationClip _bowPullClip;


    protected override void Start()
    {
        _animator = GetCachedComponent<Animator>();
        _bowPullClip = _animator.runtimeAnimatorController.animationClips.FirstOrDefault(x => x.name == "BowPull");
    }

    // 100 : x = 0.48 : y
    // 0.48x = 100y;
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnCollisionEnter Arrow");

        GameObject ob = other.gameObject;

        if (ob.CompareTag("Arrow"))
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

        if (ob.CompareTag("Arrow") && !Nocked)
        {
            CurrentArrow = null;

            TriggerEventSet(false);
        }
    }

    private void TriggerEventSet(bool addOrRemove)
    {
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonDownEvent(StartNockArrow, addOrRemove);
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonPressEvent(NockedArrowUpdate, addOrRemove);
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonUpEvent(EndNockArrow, addOrRemove);
    }

    private void StartNockArrow()
    {
        Debug.Log("StartNockArrow");

        ArrowShoundPackige.NockShoundPlay();
_animator.Play("BowPull");
        Nocked = true;
    }

    private void EndNockArrow()
    {
        Debug.Log("EndNockArrow");

        Nocked = false;

        ArrowScript arrowScript = CurrentArrow.GetComponent<ArrowScript>();
        arrowScript.Shoot(1000);

        ArrowShoundPackige.ShotShoundPlay();

        TriggerEventSet(false);

        Destroy(CurrentArrow.gameObject, 10);

        CurrentArrow = null;
    }

    private void NockedArrowUpdate()
    {
        if (!Nocked)
            return;

        CurrentArrow.transform.rotation = DelegatePosition.transform.rotation;

        DelegatePosition.transform.position = CurrentArrow.transform.position;

        float currnetPos = DelegatePosition.transform.localPosition.z;

        if (currnetPos == _shoundPos)
        {
            Debug.Log("PullShoundPlay");

            ArrowShoundPackige.PullShoundPlay();
        }

        if (currnetPos > _nockMaxPos)
            currnetPos = _nockMaxPos;
        else if (currnetPos < _nockStartPos)
            currnetPos = _nockStartPos;

        ArrowSyncBow(currnetPos - _nockStartPos);

        DelegatePosition.transform.localPosition = new Vector3(0, 0, currnetPos);

        CurrentArrow.transform.position = DelegatePosition.transform.position;
    }

    private void ArrowSyncBow(float currnetPos)
    {
        _animator.speed = (currnetPos - _bowPullClip.length) * _acceleration;
    }
}