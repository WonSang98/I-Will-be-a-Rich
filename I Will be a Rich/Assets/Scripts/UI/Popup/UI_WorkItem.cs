using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WorkItem : UI_Base
{
    enum Texts
    {
        Title_Text,
        Description_Text,
        Level_Text,
        Cost_Text,
        Value_Text,
        NowValue_Text,
    }

    enum Buttons
    {
        Upgrade_Button,
    }

    public int _needUpgradeCost { get; private set; }
    public int _nowValue { get; private set; }
    public int _nextValue { get; private set; }

    StatData _workdata;

    public void Awake()
    {
        if (Managers.Data.Stats.TryGetValue((int)Define.StatType.work + 1, out _workdata))
        {
            Debug.Log("Success Load StatData" + gameObject.name);
        }
        else
        {
            Debug.Log("Failed Load StatData" + gameObject.name);
        }
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("WorkItem Init");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetWorkUpgradeCost();
        GetWorkValue();
        GetNextValue();

        GetButton((int)Buttons.Upgrade_Button).gameObject.BindEvent(OnClickUpgradeButton);

        GetText((int)Texts.Title_Text).text = Managers.GetText(1000);
        GetText((int)Texts.Description_Text).text = Managers.GetText(1001);
        GetText((int)Texts.Level_Text).text = Managers.GetText(1002) + Managers.Game.WorkAbility;
        GetText((int)Texts.Cost_Text).text = _needUpgradeCost.ToString();
        GetText((int)Texts.Value_Text).text = _nextValue.ToString();
        GetText((int)Texts.NowValue_Text).text = _nowValue.ToString() + Managers.GetText(1003);

        return true;
    }

    void OnClickUpgradeButton()
    {
        //소지 금액 체크
        if(Managers.Game.Money >= _needUpgradeCost)
        {
            Managers.Game.Money -= _needUpgradeCost;
            Managers.Game.WorkAbility += 1;
            GetWorkUpgradeCost();
            GetWorkValue();
            GetNextValue();
            GetText((int)Texts.Cost_Text).text = _needUpgradeCost.ToString();
            GetText((int)Texts.Value_Text).text = _nowValue.ToString();
            GetText((int)Texts.Level_Text).text = Managers.GetText(1002) + Managers.Game.WorkAbility;
            GetText((int)Texts.Value_Text).text = _nextValue.ToString();
        }
        else
        {
            Debug.Log("소지 금액 부족!");
        }

    }

    void GetWorkUpgradeCost()
    {
        _needUpgradeCost = (int)(_workdata.price * Mathf.Pow(_workdata.increasePrice, Managers.Game.WorkAbility + 1));
    }

    void GetWorkValue()
    {

        if(_workdata.increaseValue == 1)
        {
            _nowValue = _workdata.value * Managers.Game.WorkAbility;
        }
        else if(_workdata.increaseValue > 1)
        {
            _nowValue = (int)(_workdata.value * Mathf.Pow(_workdata.increaseValue, Managers.Game.WorkAbility + 1));
        }
    }

    void GetNextValue()
    {
        _nextValue = (int)(_nowValue * _workdata.increaseValue);
    }

}
