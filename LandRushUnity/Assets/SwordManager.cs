using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class SwordManager : MonoBehaviourEx
{
    #region outlets

    public enum Grade
    {
        OldSword = 0,
        SteelSword = 1,
        KnightSword = 2,
        DragonSword = 3
    }

    public Grade grade = Grade.OldSword;
    public List<GameObject> SwordPrefabs;
    #endregion

    #region fields

    [SerializeField] private WaitForSeconds _wait;
    #endregion

    #region messages
    protected override void Start()
    {

    }

    protected override void Update()
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
            case Grade.OldSword:
                Instantiate(SwordPrefabs[0]);
                break;
            case Grade.SteelSword:
                Destroy(SwordPrefabs[0]);
                Instantiate(SwordPrefabs[1]);
                break;
            case Grade.KnightSword:
                Destroy(SwordPrefabs[1]);
                Instantiate(SwordPrefabs[2]);
                break;
            case Grade.DragonSword:
                Destroy(SwordPrefabs[2]);
                Instantiate(SwordPrefabs[3]);
                break;


        }
    }

    #endregion
}