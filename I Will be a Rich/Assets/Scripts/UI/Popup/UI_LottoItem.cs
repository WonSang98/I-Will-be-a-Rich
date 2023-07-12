using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LottoItem : UI_Base
{
    enum Texts
    {
        Title_Text,
        Count_Text,
        Description_Text,
        Buy_Text,
        Time_Text,
    }

    enum Buttons
    {
        Buy_Button,
        Description_Button
    }

    enum Images
    {
        Progress_Image,
    }

    Define.LottoType _type; //어떤 로또인지
    int _cost;
    float _cool;
    LottoData _lottodata;

    Action _actionBuy;
    public override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        return true;
    }

    public void SetInfo(int type)
    {
        _type = (Define.LottoType)type;

        //UI_BASE에 enum 이름으로 object 찾아서 바인딩

        if (Managers.Data.Lottos.TryGetValue((int)_type + 1, out _lottodata))
        {
            _cost = _lottodata.price;
            _cool = _lottodata.coolTime;
        }
        else
        {
            Debug.Log("Failed Load Lotto Data!");
        }

        _actionBuy -= OnClickBuyButton;
        _actionBuy += OnClickBuyButton;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        GetButton((int)Buttons.Buy_Button).gameObject.BindEvent(() => Utils.BuyItem(_cost, _actionBuy));
        GetButton((int)Buttons.Description_Button).gameObject.BindEvent(OnClickDescriptionButton);

        GetText((int)Texts.Title_Text).text = Managers.GetText(2000 + ((int)_type) * 10);
        GetText((int)Texts.Description_Text).text = Managers.GetText(2001 + ((int)_type) * 10);
        GetText((int)Texts.Buy_Text).text = Managers.GetText(2100);
        GetText((int)Texts.Count_Text).text = Managers.Game.LottoCounts[(int)_type].ToString();
        GetText((int)Texts.Time_Text).text = TimeTransfer(Managers.Game.LottoCoolTimes[(int)_type]);


        GetImage((int)Images.Progress_Image).fillAmount = Managers.Game.LottoCoolTimes[(int)_type];
    }

    void OnClickBuyButton()
    {
        Debug.Log($"Buy Lotto{(int)_type}");
        Managers.Game.LottoCounts[(int)_type] += 1;
        RefreshCount();
        RefreshTime();
    }
    void OnClickDescriptionButton()
    {
        //TODO
        Debug.Log("DESCRIPTION!");
    }

    string TimeTransfer(float time)
    {
        int min, sec;
        min = (int)time / 60;
        sec = (int)time % 60;


        if(min == 0)
        {
            return $"{sec}S";
        }
        else
        {
            return $"{min}M {sec}S";
        }
    }

    public void RefreshTime()
    {
        GetImage((int)Images.Progress_Image).fillAmount = Managers.Game.LottoCoolTimes[(int)_type] / _cool;
        GetText((int)Texts.Time_Text).text = TimeTransfer(Managers.Game.LottoCoolTimes[(int)_type]);
    }

    public void RefreshCount()
    {
        GetText((int)Texts.Count_Text).text = Managers.Game.LottoCounts[(int)_type].ToString();
    }

}
