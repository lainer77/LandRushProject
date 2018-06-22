using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityScriptHelper;

public class CharacterControllerDevice : MonoBehaviourEx
{

    #region outlets

    public float Speed;
    public AudioClip StepSound;
    #endregion

    #region fields

    private Rigidbody _rigidbody;
    private AudioSource _audio;
    private Transform _camTransform;
    private CharacterController _characterController;

    #endregion

    #region messages

    protected override void OnDestroy()
    {
        ControllSetting(false);
    }

    private DeviceInteraction _leftController;

    protected override void Start()
    {
        _leftController = DeviceRepository.LeftDeviceInteraction;
        _rigidbody = GetCachedComponent<Rigidbody>();
       // ControllSetting(true);
        _audio = GetComponent<AudioSource>();
        _camTransform = Camera.main.GetComponent<Transform>();
        _characterController = GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        Move();
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

    private void Move()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            MoveUp();
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            MoveDown();
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();
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
#pragma warning disable 618
        Vector3 dir = _camTransform.TransformDirection(vector);
#pragma warning restore 618
         transform.Translate(dir * Speed * Time.deltaTime) ;
        _audio.PlayOneShot(StepSound);
    }

    #endregion
}