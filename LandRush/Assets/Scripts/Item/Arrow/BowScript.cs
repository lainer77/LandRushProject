using System.Collections;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;

public class BowScript : EquipmentItemScript
{
    private DeviceInteraction _rightDeviceInteraction;
    public ArrowShoundPackige ArrowShoundPackige;
    public GameObject CurrentArrow { get; set; }
    public GameObject DelegatePosition;
    public GameObject Boll;
    private GameObject _rightHand;

    public bool Nocked { get; set; }

    public GameObject StartPosition;

    private Vector3 _power;
    private GameObject _git;
    private Rigidbody _arrowRigidbody;
    private float _nockStartPos = 0.2f;
    private float _nockMaxPos = 0.7f;


    protected override ItemID GetInstanceItemId()
    {
        return ItemID.OldBow;
    }

    protected override void Start()
    {
        _rightDeviceInteraction = DeviceRepository.RightDeviceInteraction;
        _rightHand = _rightDeviceInteraction.transform.Find("Hand").gameObject;
        _rightDeviceInteraction.StrongVibrationTime(10f);
    }

    // 100 : x = 0.48 : y
    // 0.48x = 100y;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Arrow))
        {
            CurrentArrow = other.gameObject;

            TriggerEventSet(true);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Arrow) && !Nocked)
        {
            CurrentArrow = null;

            TriggerEventSet(false);
        }
    }

    private void TriggerEventSet(bool addOrRemove)
    {
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonDownEvent(StartNockArrow, addOrRemove);
//        _rightDeviceInteraction.TriggerButton.SetDeviceButtonPressEvent(NockedArrowUpdate, addOrRemove);
        _rightDeviceInteraction.TriggerButton.SetDeviceButtonUpEvent(EndNockArrow, addOrRemove);
    }

    private void StartNockArrow()
    {
        ArrowShoundPackige.NockShoundPlay();
        Nocked = true;
        ArrowScript aScript = CurrentArrow.GetComponent<ArrowScript>();
        _git = aScript.Git;
        _arrowRigidbody = aScript.Rigid;

        CurrentArrow.transform.parent = transform;

        StartCoroutine("FixedUpdateArrow");
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

        StopCoroutine("FixedUpdateArrow");
    }

    private IEnumerator FixedUpdateArrow()
    {
        while (Nocked)
        {
            if (CurrentArrow == null)
                yield break;
            NockedArrowUpdate();
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void NockedArrowUpdate()
    {
        //현재 Nocked 된 화살의 위치를 시위대에 고정시킨다.(위치, 방향)
        //        CurrentArrow.transform.rotation = DelegatePosition.transform.rotation;
        _rightHand.transform.rotation = DelegatePosition.transform.rotation;

        DelegatePosition.transform.position = _rightHand.transform.position;

        //화살의 현재 위치
        float arrowCurrentPos = DelegatePosition.transform.localPosition.z;
        if (arrowCurrentPos > _nockMaxPos)
        {
            arrowCurrentPos = _nockMaxPos;
            _rightDeviceInteraction.StrongVibrationTime(1f);
        }
        else if (arrowCurrentPos < _nockStartPos)
        {
            arrowCurrentPos = _nockStartPos;
            _rightDeviceInteraction.StrongVibrationTime(1f);
        }

        BowStringSync();

        DelegatePosition.transform.localPosition = Vector3.forward * arrowCurrentPos;

        _rightHand.transform.position = DelegatePosition.transform.position;

        CurrentArrow.transform.position = DelegatePosition.transform.localPosition;
        //////////////////////////////////////////////////////////////////////////////
    }

    private void BowStringSync()
    {
        Boll.transform.position = _git.transform.position;
        _power = StartPosition.transform.position - Boll.transform.position;
    }
}