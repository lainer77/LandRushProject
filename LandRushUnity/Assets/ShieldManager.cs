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
        KnightShield = 2,
        IronShield =3
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
                Instantiate(ShieldPrefabs[0]);
                break;
            case Grade.WarShield:
                Destroy(ShieldPrefabs[0]);
                Instantiate(ShieldPrefabs[1]);
                break;
            case Grade.KnightShield:
                Destroy(ShieldPrefabs[1]);
                Instantiate(ShieldPrefabs[2]);
                break;
            case Grade.IronShield:
                Destroy(ShieldPrefabs[2]);
                Instantiate(ShieldPrefabs[3]);
                break;
                

        }
    }

    #endregion
}
