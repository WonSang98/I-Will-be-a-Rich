using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Upgrade : UI_Base
{
    protected enum Texts
    {
        Title_Text,
        Description_Text,
        Level_Text,
        Value_Text,
        Cost_Text,
        NowValue_Text
    }

    protected enum Buttons
    {
        Upgrade_Button,
    }

    protected enum Images
    {
        Icon,
    }

    protected Define.Upgrade _type;
    protected UpgradeData _data;

    protected int _upgradeCost;
    protected float _nowValue;
    protected float _nextValue;

    public Action _actionUpgrade;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SetType();
        Managers.Data.Upgrades.TryGetValue((int)_type + 1, out _data);
        GetDatas();

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        _actionUpgrade -= OnClickUpgradeButton;
        _actionUpgrade += OnClickUpgradeButton;

        RefreshUI();
        SetIconImage();
        GetButton((int)Buttons.Upgrade_Button).gameObject.BindEvent(() => Utils.BuyItem(_upgradeCost, _actionUpgrade));

        return true;
    }

    virtual protected void SetType()
    {

    }

    //Action
    public void OnClickUpgradeButton()
    {
        //1. 레벨업
        //2. 수치 조정
        //3. UI 조정
        Managers.Game.Upgrades[(int)_type] += 1;
        GetDatas();
        RefreshUI();
    }

    protected void SetIconImage()
    {
        //TODO
    }

    protected void RefreshUI()
    {
        GetText((int)Texts.Title_Text).text = Managers.GetText(3000 + (int)_type);
        GetText((int)Texts.Description_Text).text = Managers.GetText(3010 + (int)_type);
        GetText((int)Texts.Level_Text).text = Managers.GetText(3100) + " " + Managers.Game.Upgrades[(int)_type];
        GetText((int)Texts.Cost_Text).text = _upgradeCost.ToString();
        GetText((int)Texts.NowValue_Text).text = _nowValue.ToString() + "%";
        GetText((int)Texts.Value_Text).text = _nowValue.ToString() + "%";
    }


    protected void GetDatas()
    {
        GetUpgradeCost();
        GetNowValue();
        GetNextValue();
    }

    protected void GetUpgradeCost()
    {
        _upgradeCost = (int)(_data.price * (_data.increasePrice * Managers.Game.Upgrades[(int)_type]));
    }

    protected void GetNowValue()
    {
        _nowValue = (_data.value * (_data.increaseValue * Managers.Game.Upgrades[(int)_type]));
    }

    protected void GetNextValue()
    {
        _nextValue = (_data.value * (_data.increaseValue * (Managers.Game.Upgrades[(int)_type]+1)));
    }
}
