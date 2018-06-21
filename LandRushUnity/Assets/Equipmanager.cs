using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Repository;
using UnityEngine;
using UnityScriptHelper;

public class Equipmanager : MonoBehaviourEx
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
public List<GameObject> Prefabs;
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
    Instantiate(Prefabs[0], transform.position, Prefabs[0].transform.rotation, transform);
    break;
    case Grade.Grade1:
    Destroy(Prefabs[0].gameObject);
    Instantiate(Prefabs[1], transform.position, Prefabs[1].transform.rotation, transform);
    break;
    case Grade.Grade2:
    Destroy(Prefabs[1].gameObject);
    Instantiate(Prefabs[2], transform.position, Prefabs[2].transform.rotation, transform);
    break;
    case Grade.Grade3:
    Destroy(Prefabs[2].gameObject);
    Instantiate(Prefabs[3], transform.position, Prefabs[3].transform.rotation, transform);
    break;


}
}

#endregion
}