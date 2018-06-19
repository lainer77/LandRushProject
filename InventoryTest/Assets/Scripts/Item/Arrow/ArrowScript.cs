using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;

public class ArrowScript : EquipmentItemScript
{
    public enum ArrowState
    {
        Shoot,
        Hand,
        Nocked
    }


    public Rigidbody Head;
    private GameObject _git;

    public GameObject Git
    {
        get
        {
            if (_git == null)
                _git = transform.Find("Git").gameObject;
            return _git;
        }
    }

    public Rigidbody Rigid { get; private set; }
    private ArrowState _state;

    public ArrowState State
    {
        get { return _state; }
        set
        {
            _state = value;
            if (_state == ArrowState.Hand)
            {
                UnUseGravity(Rigid);
                UnUseGravity(Head);
            }
            else if (_state == ArrowState.Nocked)
            {
            }
            else
            {
                UseGravity(Rigid);
                UseGravity(Head);
            }
        }
    }

    protected override ItemID GetInstanceItemId()
    {
        return ItemID.Arrow;
    }

    private readonly float _speed = 100;
    private readonly float _nockStartPos = 0.2f;
    private readonly float _nockMaxPos = 0.7f;

    protected override void Start()
    {
        _state = ArrowState.Hand;
        Rigid = GetCachedComponent<Rigidbody>();
    }

    protected override void OnTransformParentChanged()
    {
        if (transform.parent != null && transform.parent.CompareTag(Tags.RightController))
        {
            State = ArrowState.Hand;
        }
    }

    public void Shoot(Vector3 power)
    {
        transform.SetParent(null);

        State = ArrowState.Shoot;


        Rigid.velocity = power * _speed / 5;
        Head.velocity = power * _speed;

        Destroy(gameObject, 10);
    }


    #region GravitySwitch

    private void UseGravity(Rigidbody rigid)
    {
        rigid.isKinematic = false;
        rigid.useGravity = true;
    }

    private void UnUseGravity(Rigidbody rigid)
    {
        rigid.isKinematic = true;
        rigid.useGravity = false;
    }

    #endregion
}