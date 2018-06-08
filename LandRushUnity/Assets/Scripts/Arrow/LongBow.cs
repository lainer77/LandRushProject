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
    private bool _nocked = false;
    public bool Nocked
    {
        get
        {
            return _nocked;
        }
        set
        {
            _nocked = value;
            _animator.SetBool("Pull", _nocked);
        }
    }
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
        _rightDeviceInteraction = DeviceRepository.RightDeviceInteraction;
    }

    // 100 : x = 0.48 : y
    // 0.48x = 100y;
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnCollisionEnter Arrow");
        

        if (other.CompareTag("Arrow"))
        {
            CurrentArrow = other.gameObject;

            TriggerEventSet(true);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        Debug.Log("OnCollisionExit Arrow");
        

        if (other.CompareTag("Arrow") && !Nocked)
        {
            Debug.Log("Arrow is null");
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
        Nocked = true;
    }

    private void EndNockArrow()
    {
        Debug.Log("EndNockArrow");


        ArrowScript arrowScript = CurrentArrow.GetComponent<ArrowScript>();
        arrowScript.Shoot(1000);

        ArrowShoundPackige.ShotShoundPlay();

        TriggerEventSet(false);

        Destroy(CurrentArrow.gameObject, 10);

        CurrentArrow = null;
        Nocked = false;
    }

    private void NockedArrowUpdate()
    {
        if (!Nocked)
            return;
        if(CurrentArrow == null)
            return;
        
        CurrentArrow.transform.rotation = DelegatePosition.transform.rotation;

        DelegatePosition.transform.position = CurrentArrow.transform.position;

        float arrowCurrnetPos = DelegatePosition.transform.localPosition.z;

        if (arrowCurrnetPos.Equals(_shoundPos))
        {
            Debug.Log("PullShoundPlay");

            ArrowShoundPackige.PullShoundPlay();
        }

        if (arrowCurrnetPos > _nockMaxPos)
        {
            arrowCurrnetPos = _nockMaxPos;
            _rightDeviceInteraction.StrongVibrationTime(0.3f);
        }
        else if (arrowCurrnetPos < _nockStartPos)
        {
            arrowCurrnetPos = _nockStartPos;
        }

        ArrowSyncBow(arrowCurrnetPos - _nockStartPos);

        DelegatePosition.transform.localPosition = new Vector3(0, 0, arrowCurrnetPos);

        CurrentArrow.transform.position = DelegatePosition.transform.position;
    }

    private void ArrowSyncBow(float currnetPos)
    {
        _animator.speed = (currnetPos - _bowPullClip.length) * _acceleration;
    }
}