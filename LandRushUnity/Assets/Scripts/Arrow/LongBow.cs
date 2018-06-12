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
    public GameObject Boll;
    private GameObject _git;

    public bool Nocked { get; set; }

    public GameObject StartPosition;

    private Vector3 _power;
    private float _nockStartPos = 0.2f;
    private float _nockMaxPos = 0.7f;


    protected override void Start()
    {
        _rightDeviceInteraction = DeviceRepository.RightDeviceInteraction;
    }

    // 100 : x = 0.48 : y
    // 0.48x = 100y;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            CurrentArrow = other.gameObject;

            TriggerEventSet(true);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Arrow") && !Nocked)
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
        ArrowShoundPackige.NockShoundPlay();
        Nocked = true;
        _git = CurrentArrow.GetComponent<ArrowScript>().Git;
    }

    private void EndNockArrow()
    {
        if (CurrentArrow == null)
            return;

        ArrowScript arrowScript = CurrentArrow.GetComponent<ArrowScript>();

        arrowScript.Shoot(_power);

        ArrowShoundPackige.ShotShoundPlay();

        TriggerEventSet(false);

//        Destroy(CurrentArrow.gameObject, 10);
        _git = null;
        CurrentArrow = null;
        Nocked = false;
    }

    private void NockedArrowUpdate()
    {
        if (!Nocked)
            return;
        if (CurrentArrow == null)
            return;

        //현재 Nocked 된 화살의 위치를 시위대에 고정시킨다.(위치, 방향)
        CurrentArrow.transform.rotation = DelegatePosition.transform.rotation;

        DelegatePosition.transform.position = CurrentArrow.transform.position;

        //화살의 현재 위치
        float arrowCurrentPos = DelegatePosition.transform.localPosition.z;


        if (arrowCurrentPos >= 0.3f && arrowCurrentPos <= 0.35f)
        {
            ArrowShoundPackige.PullShoundPlay();
            _rightDeviceInteraction.StrongVibrationTime(0.1f);
        }
        if (arrowCurrentPos >= 0.6f && arrowCurrentPos <= 0.65f)
        {
            ArrowShoundPackige.PullShoundPlay();
            _rightDeviceInteraction.StrongVibrationTime(0.1f);
        }

        if (arrowCurrentPos > _nockMaxPos)
        {
            arrowCurrentPos = _nockMaxPos;
            _rightDeviceInteraction.StrongVibrationTime(0.3f);
        }
        else if (arrowCurrentPos < _nockStartPos)
        {
            arrowCurrentPos = _nockStartPos;
        }

        BowStringSync();

        DelegatePosition.transform.localPosition = new Vector3(0, 0, arrowCurrentPos);

        CurrentArrow.transform.position = DelegatePosition.transform.position;
    }

    private void BowStringSync()
    {
        Boll.transform.position = _git.transform.position;
        _power = StartPosition.transform.position - Boll.transform.position;
    }
}