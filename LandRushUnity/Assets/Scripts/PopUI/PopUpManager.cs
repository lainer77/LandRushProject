using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class PopUpManager : MonoBehaviourEx
{
    public GameObject PopUICanvas;
    private DeviceInteraction _rightInteraction;

    #region messages
    protected override void Start()
    {
        _rightInteraction = DeviceRepository.RightDeviceInteraction;
        _rightInteraction.ApplicationMenuButton.DeviceButtonDown += OnAppMenuButtonDowm;
    }

    protected override void Update()
    {

    }

    #endregion	

    #region methods

    private void OnAppMenuButtonDowm()
    {
        if (PopUICanvas.activeSelf)
            PopUICanvas.SetActive(false);
        else
            PopUICanvas.SetActive(true);
    }
    #endregion
}
