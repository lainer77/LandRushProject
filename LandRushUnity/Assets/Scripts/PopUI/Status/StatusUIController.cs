using System.Collections;
using System.Collections.Generic;
using LandRushLibrary.Units;
using UnityEngine;
using UnityEngine.UI;
using UnityScriptHelper;

public class StatusUIController : MonoBehaviourEx
{
    #region outlets
    
    #endregion

    #region fields

    public GameObject LevelText;
    public GameObject HpText;
    public GameObject AttackText;
    public GameObject ArmorText;
    public GameObject ExpInfoText;
    public GameObject ExpValue;

    private Text _levelText;
    private Text _hpText;
    private Text _attackText;
    private Text _armorText;
    private Text _expText;

    private Player _player;
    #endregion

    #region messages
	protected override void Start ()
	{
	    _levelText = LevelText.GetComponent<Text>();
        _hpText = HpText.GetComponent<Text>();
        _attackText = AttackText.GetComponent<Text>();
        _armorText = ArmorText.GetComponent<Text>();
        _expText = ExpInfoText.GetComponent<Text>();

        _player = Player.Instance;

        UpdateStatus();
    }

    public void UpdateStatus()
    {
        _levelText.text = _player.Level.ToString();
        _hpText.text = $"{_player.CurrentHp} / {_player.MaxHp}";
        _attackText.text = _player.AttackPower.ToString();
        _armorText.text = _player.Armor.ToString();
        _expText.text = $"{_player.CurrentExp} / {_player.MaxExp}  { (double)_player.CurrentExp / _player.MaxExp:N02}%";

        ExpValue.GetComponent<Image>().fillAmount = (float) _player.CurrentExp / _player.MaxExp;

    }
    #endregion	

    #region methods
    
    #endregion
}
