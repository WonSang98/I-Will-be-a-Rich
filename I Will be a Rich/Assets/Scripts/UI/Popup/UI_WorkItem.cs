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
    }

    enum Buttons
    {
        Upgrade_Button,
    }

    public int _needUpgradeCost { get; private set; }
    public int _nowValue { get; private set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("WorkItem Init");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetWorkUpgradeCost();
        GetWorkValue();

        GetButton((int)Buttons.Upgrade_Button).gameObject.BindEvent(OnClickUpgradeButton);

        GetText((int)Texts.Title_Text).text = Managers.GetText(1000);
        GetText((int)Texts.Description_Text).text = Managers.GetText(1001);
        GetText((int)Texts.Level_Text).text = Managers.GetText(1002) + Managers.Game.WorkAbility;
        GetText((int)Texts.Cost_Text).text = _needUpgradeCost.ToString();
        GetText((int)Texts.Value_Text).text = _nowValue.ToString();


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
            GetText((int)Texts.Cost_Text).text = _needUpgradeCost.ToString();
            GetText((int)Texts.Value_Text).text = _nowValue.ToString();
        }
        else
        {
            Debug.Log("소지 금액 부족!");
        }

    }

    void GetWorkUpgradeCost()
    {
        if (Managers.Data.Stats.TryGetValue((int)Define.StatType.work + 1, out StatData value) == false) return;

        _needUpgradeCost =  value.price * (int)Mathf.Pow(value.increasePrice, Managers.Game.WorkAbility);
    }

    void GetWorkValue()
    {
        if (Managers.Data.Stats.TryGetValue((int)Define.StatType.work + 1, out StatData value) == false) return;

        if(value.increaseValue == 1)
        {
            _nowValue = value.value * Managers.Game.WorkAbility;
        }
        else if(value.increaseValue > 1)
        {
            _nowValue = value.value * (int)Mathf.Pow(value.increaseValue, Managers.Game.WorkAbility + 1);
        }
    }


}
