using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AlertPopup : UI_Popup
{
    enum Texts
    {
        Alert_Text,
        Sure_Text
    }

    enum Buttons
    {
        Sure_Button,
    }

    Action _clickEventCallBack;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public void SetInfo(int alertNumber, Action onClickCallBack = null)
    {
        Debug.Log("ALERT");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Sure_Button).gameObject.BindEvent(OnClickSureButton);

        GetText((int)Texts.Alert_Text).text = Managers.GetText(alertNumber); 
        GetText((int)Texts.Sure_Text).text = Managers.GetText(alertNumber+100);

        _clickEventCallBack = onClickCallBack;
    }

    void OnClickSureButton()
    {
        Managers.UI.ClosePopupUI(this);
        _clickEventCallBack?.Invoke();
    }

}
