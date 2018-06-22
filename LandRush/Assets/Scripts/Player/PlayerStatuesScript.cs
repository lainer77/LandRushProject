using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.PlayerItemManagers;
using LandRushLibrary.Repository;
using LandRushLibrary.Units;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class PlayerStatuesScript : MonoBehaviourEx
{
    private Player _player;
    public Image Hp;
    public Image Exp;
    public Text ArrowNum;
    public Text PotionNum;
    private PlayerInventory _inventory;

    // Use this for initialization
    protected override void Start()
    {
        _player = Player.Instance;
        _inventory = PlayerInventory.Instance;
        StartCoroutine(UI_Update());
    }

    // MaxHP = 20
    // 20 : X = 1 : Y
    // X = 20Y
    // Y = X/20
    // Update is called once per frame

    protected IEnumerator UI_Update()
    {
        while (true)
        {
            float cur = _player.CurrentHp;
            cur = cur / _player.MaxHp;
            Hp.fillAmount = cur;

            cur = _player.CurrentExp;
            cur = cur / _player.MaxExp;
            Exp.fillAmount = cur;

            if (_player.Equipment.RightEquipment != null)
                ArrowNum.text = _player.Equipment.RightEquipment.Type == ItemType.Arrow
                    ? _player.Equipment.RightEquipment.Amount.ToString()
                    : "0";

            PotionNum.text = _inventory.GetAmountForId(ItemID.HpPotion).ToString();
            yield return new WaitForSeconds(0.3f);
        }
    }
}