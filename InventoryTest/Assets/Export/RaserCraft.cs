using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;
using VolumetricLines;

public class RaserCraft : MonoBehaviourEx
{
    #region outlets

    public GameObject VolumetricLine;
    public GameObject RayResult;

    public RaycastHit Hit => _hit;

    private RaycastHit _hit;

    #endregion

    #region fields

    /// <summary>
    /// 거리에 따른 스케일 변화를 위한 오브젝트 대상
    /// </summary>
//    private GameObject _scaleDistance;
    private VolumetricLineBehavior _volumetricLineBehavior;

    /// <summary>
    /// 충돌하는 위치에 출력할 결과(충돌 파티클)
    /// 레이캐스팅을 쏘는 위치
    /// </summary>
    private GameObject _raybody;

    #endregion

    private DeviceInteraction _device;

    #region messages

    protected override void Start()
    {
        _device = GetCachedComponent<DeviceInteraction>();

        _volumetricLineBehavior = VolumetricLine.GetComponent<VolumetricLineBehavior>();
    }

    protected override void FixedUpdate()
    {
        Raser();
    }

    public void Raser()
    {
        Physics.Raycast(transform.position, transform.forward, out _hit, 10);

        _volumetricLineBehavior.EndPos = new Vector3(0, 0, Hit.distance * 1000);

        RayResult.transform.position = Hit.point;
        if (Hit.normal == Vector3.zero)
            return;
        RayResult.transform.rotation = Quaternion.LookRotation(Hit.normal);
    }

    #endregion

    #region methods

    #endregion
}