using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScriptHelper;

public class SwordManager : MonoBehaviourEx
{
    #region outlets

    public enum Grade
    {
        Grade0,
        Grade1,
        Grade2,
        Grade3
    }

    public Grade grade = Grade.Grade0;
    public List<GameObject> SwordPrefabs;
    #endregion

    #region fields

    private WaitForSeconds _wait;
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
            case Grade.Grade0:
                Instantiate(SwordPrefabs[0], transform.position, SwordPrefabs[0].transform.rotation,transform);
                break;
            case Grade.Grade1:
                Destroy(SwordPrefabs[0]);
                Instantiate(SwordPrefabs[1], transform.position, SwordPrefabs[1].transform.rotation,transform);
                break;
            case Grade.Grade2:
                Destroy(SwordPrefabs[1]);
                Instantiate(SwordPrefabs[2], transform.position, SwordPrefabs[2].transform.rotation,transform);
                break;
            case Grade.Grade3:
                Destroy(SwordPrefabs[2]);
                Instantiate(SwordPrefabs[3], transform.position, SwordPrefabs[3].transform.rotation,transform);
                break;


        }
    }

    #endregion
}