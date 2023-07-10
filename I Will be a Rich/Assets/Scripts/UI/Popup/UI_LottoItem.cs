using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LottoItem : UI_Popup
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

    public Define.LottoType type; //어떤 로또인지
    int _cost;

    public override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        //UI_BASE에 enum 이름으로 object 찾아서 바인딩

        if(Managers.Data.Lottos.TryGetValue((int)type+1, out LottoData lottodata))
        {
            _cost = lottodata.price;
        }
        else
        {
            Debug.Log("Failed Load Lotto Data!");
        }


        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        GetButton((int)Buttons.Buy_Button).gameObject.BindEvent(OnClickBuyButton);
        GetButton((int)Buttons.Buy_Button).gameObject.BindEvent(OnPressBuyButton, Define.UIEvent.Pressed);
        GetButton((int)Buttons.Description_Button).gameObject.BindEvent(OnClickDescriptionButton);

        GetText((int)Texts.Title_Text).text = Managers.GetText(2000 + (int)type);
        GetText((int)Texts.Description_Text).text = Managers.GetText(2001 + ((int)type) * 10);
        GetText((int)Texts.Buy_Text).text = Managers.GetText(2100);
        GetText((int)Texts.Count_Text).text = Managers.Game.LottoCounts[(int)type].ToString();
        GetText((int)Texts.Time_Text).text = TimeTransfer(Managers.Game.LottoCoolTimes[(int)type]);


        GetImage((int)Images.Progress_Image).fillAmount = Managers.Game.LottoCoolTimes[(int)type];


        return true;
    }

    void OnClickBuyButton()
    {
        if(Managers.Game.Money >= _cost)
        {
            Debug.Log($"Buy Lotto{(int)type}");
            Managers.Game.Money -= _cost;
            Managers.Game.LottoCounts[(int)type] += 1;
        }
        else
        {
            Debug.Log($"Failed Buy Lotto{(int)type}");
        }
    }

    void OnPressBuyButton()
    {
        if (Managers.Game.Money >= _cost * 10)
        {
            Debug.Log($"Buy Lotto{(int)type}");
            Managers.Game.Money -= _cost * 10;
            Managers.Game.LottoCounts[(int)type] += 10;
        }
        else
        {
            Debug.Log($"Failed Buy Press Lotto{(int)type}");
        }
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

}
