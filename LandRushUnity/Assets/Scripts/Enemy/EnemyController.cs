using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityScriptHelper;

public class EnemyController : MonoBehaviourEx
{
    #region outlets


    #endregion

    #region fields
    private GameObject _player;
    private Animator _animator;
    private float _distance;
    private NavMeshAgent _navMeshAgent;

    #endregion

    #region messages
    protected override void Start () 
	{
		_player = GameObject.FindWithTag("Player");
	    _animator = GetComponent<Animator>();
	    _navMeshAgent = GetComponent<NavMeshAgent>();

	}
	
	protected override void Update ()
	{
	    _distance = Vector3.Distance(_player.transform.position, transform.position);

    }
    #endregion	

    #region methods
    
    #endregion
}
