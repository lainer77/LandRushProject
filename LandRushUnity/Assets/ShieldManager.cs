using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class ShieldManager : MonoBehaviourEx
{
    #region outlets

    public enum Grade
    {
        OldShield = 0,
        WarShield = 1,
        IronShield =2,
        KnightShield = 3
    }
    public Grade grade = Grade.OldShield;
    public List<GameObject> ShieldPrefabs;
    #endregion

    #region fields

    [SerializeField] private WaitForSeconds _wait;
    #endregion

    #region messages
	protected override void Start () 
	{
		
	}
	
	protected override void Update ()
	{
		
	}

    private void OnEnable()
    {
        StartCoroutine(ChangePrefab());
    }

    #endregion	

    #region methods

    private IEnumerator ChangePrefab()
    {
        yield return _wait;

        switch (grade)
        {
            case Grade.OldShield:
                Instantiate(ShieldPrefabs[0], transform.position, ShieldPrefabs[0].transform.rotation,transform);
                break;
            case Grade.WarShield:
                Destroy(ShieldPrefabs[0]);
                Instantiate(ShieldPrefabs[1], transform.position, ShieldPrefabs[1].transform.rotation,transform);
                break;
            case Grade.IronShield:
                Destroy(ShieldPrefabs[1]);
                Instantiate(ShieldPrefabs[2], transform.position, ShieldPrefabs[2].transform.rotation, transform);
                break;
            case Grade.KnightShield:
                Destroy(ShieldPrefabs[2]);
                Instantiate(ShieldPrefabs[3], transform.position, ShieldPrefabs[3].transform.rotation, transform);
                break;
                

        }
    }

    #endregion
}
