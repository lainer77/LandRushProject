using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Debug = UnityEngine.Debug;

public class Player_Controller : MonoBehaviour
{
    enum play
    {
        HP = 100, Attack = 10, Defense = 5
    }
    #region outlets

    #endregion

    #region fields
    public PlayerInfo Player = new PlayerInfo((float)play.HP, (float)play.Attack, (float)play.Defense, (float)play.HP);
    private GameObject _hPImg;
    private GameObject _canvas;
    #endregion

    #region messages
    protected void Start()
    {
        //   Player.WeaponAttack = LeftWeapon.GetComponent<IWeapon>().Attack + RightWeapon.GetComponent<IWeapon>().Attack;
        //      Player.WeaponDefense = LeftWeapon.GetComponent<IWeapon>().Defense+ RightWeapon.GetComponent<IWeapon>().Defense;
        _hPImg = GameObject.Find("HP");
        _canvas = GameObject.Find("Canvas");
    }

    protected void Update()
    {
        _hPImg.GetComponent<Image>().fillAmount = (float)Player.HP * 0.01f;
    }

    #endregion	

    #region methods

    void OnTriggerEnter()
    {

        _canvas.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Attack");
    }


    public void Change(int i)
    {
        switch (i)
        {
            case 1:
                transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 2:
                transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
                break;
        }
    }
    #endregion
}


