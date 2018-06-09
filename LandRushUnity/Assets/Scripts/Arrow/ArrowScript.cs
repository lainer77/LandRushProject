using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Items;
using UnityEngine;
using UnityScriptHelper;

public class ArrowScript : MonoBehaviourEx 
{
    public enum ArrowState
    {
        Shoot,
        Hand,
        Nocked
    }
	public Arrow Arrow { get; set; }
    public Rigidbody Head;
	private Rigidbody _rigidbody;
    private ArrowState _state;

    public ArrowState State
    {
        get { return _state; }
        set
        {
            _state = value;
            if (_state == ArrowState.Hand)
            {
                UnUseGravity(_rigidbody);
                UnUseGravity(Head);
            }
            else if (_state == ArrowState.Nocked)
            {
                
            }
            else
            {
                UseGravity(_rigidbody);
                UseGravity(Head);
            }
        }
    }

    protected override void Start()
    {
        _state = ArrowState.Hand;
        _rigidbody = GetCachedComponent<Rigidbody>(); 
    }

    protected override void OnTransformParentChanged()
    {
        if (transform.parent != null && transform.parent.CompareTag(Tags.RightController))
        {
            State = ArrowState.Hand;
        }
    }

    public void Shoot(float power)
	{
        transform.SetParent(null);

	    State = ArrowState.Shoot;
        

	    _rigidbody.AddForce(Vector3.back * power);
	    Head.AddForce(Vector3.back * power);

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
