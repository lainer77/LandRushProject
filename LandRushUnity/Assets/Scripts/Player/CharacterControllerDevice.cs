using System.Collections;
using System.Security.Cryptography.X509Certificates;
using LandRushLibrary.Enums;
using LandRushLibrary.Items;
using LandRushLibrary.Units;
using UnityEngine;
using UnityScriptHelper;

public class CharacterControllerDevice : MonoBehaviourEx
{
    #region outlets

    public enum Status
    {
        Idle,
        Move,
        Battle,
        Dead
    }

    public Status status = Status.Idle;
    public float MoveSpeed;
    public bool OnBattle;
    public GameObject[] Hands;
    #endregion

    #region fields

    private Transform _camTransform;
    private bool _isDie;
    private WaitForSeconds _wait;

    #endregion

    #region messages

    protected override void OnDestroy()
    {
        ControllSetting(false);
    }

    private DeviceInteraction _leftController;
    private DeviceInteraction _rightController;

    protected override void Start()
    {
        _leftController = DeviceRepository.LeftDeviceInteraction;
        _rightController = DeviceRepository.RightDeviceInteraction;
        _camTransform = Camera.main.GetComponent<Transform>();
        ControllSetting(true);
    }

    protected override void Update()
    {
        

    }

    protected override void OnEnable()
    {
        StartCoroutine(CheckStatus());
    }

    #endregion

    #region methods

    public void ControllSetting(bool addOrRemove)
    {
        _leftController.TouchpadButton.SetDPadUpButtonEvent(MoveUp, addOrRemove);
        _leftController.TouchpadButton.SetDPadDownButtonEvent(MoveDown, addOrRemove);
        _leftController.TouchpadButton.SetDPadLeftButtonEvent(MoveLeft, addOrRemove);
        _leftController.TouchpadButton.SetDPadRightButtonEvent(MoveRight, addOrRemove);
    }

    private void MoveUp()
    {
        MoveTo(Vector3.forward);
    }

    private void MoveDown()
    {
        MoveTo(Vector3.back);
    }

    private void MoveLeft()
    {
        MoveTo(Vector3.left);
    }

    private void MoveRight()
    {
        MoveTo(Vector3.right);
    }

    public void MoveTo(Vector3 vector)
    {
        Status status = Status.Move;
        Vector3 dir = _camTransform.TransformDirection(vector);
        transform.Translate(dir * Time.deltaTime * MoveSpeed);
    }

    IEnumerator CheckStatus()
    {
        while (!_isDie)
        {
            yield return _wait;

            switch (status)
            {
                case Status.Idle:
                    SetHands(true);
                    break;
                case Status.Move:
                    MoveSpeed = 3;
                    break;
                case Status.Battle:
                    SetHands(false);
                    SetWeapons(true);
                    break;
                case Status.Dead:
                    SetHands(true);
                    MoveSpeed = 0;
                    OnBattle = false;
                    break;
            }
        }
            
    }

    public void OnBattleMode(bool b)
    {

    }
    private void SetWeapons(bool b)
    {
        
    }


    public void SetHands(bool b)
    {
        if (b)
        {
            for (int i = 0; i < Hands.Length; i++)
            {
                Hands[i].SetActive(b);
            }
        }
        else
        {
            for (int i = 0; i < Hands.Length; i++)
            {
                Hands[i].SetActive(b);
            }
        }
    }

    #endregion
}