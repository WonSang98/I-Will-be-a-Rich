using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class UI_InputNamePopup : UI_Popup
{
    enum GameObjects
    {
        NameInput,
    }

    enum Texts
    {
        Submit_Text,
        NamePlaceholder,
        Guide_Text,
    }

    enum Buttons
    {
        Submit_Button,
    }

    TMP_InputField _inputField;

    public override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Submit_Button).gameObject.BindEvent(OnClickSubmitButton);

        GetText((int)Texts.Guide_Text).text = Managers.GetText(30002);
        GetText((int)Texts.NamePlaceholder).text = Managers.GetText(30001);
        GetText((int)Texts.Submit_Text).text = Managers.GetText(30000);

        _inputField = GetObject((int)GameObjects.NameInput).gameObject.GetComponent<TMP_InputField>();
        _inputField.text = "";

        return true;
    }

    void OnClickSubmitButton()
    {
        //TODO : »ç¿îµå
        NickNammeCheck(_inputField.text);
    }

    void NickNammeCheck(string nickname)
    {
        Regex nickregex = new Regex(@"^(?=.*[a-z0-9°¡-ÆR])[a-z0-9°¡-ÆR]{2,8}$");
        if (nickregex.IsMatch(nickname))
        {
            Action confimedCallBack = ConfirmNickName;
            Managers.UI.ShowPopupUI<UI_AlertPopup>().SetInfo(200, confimedCallBack);
        }
        else
        {
            //¾ÈµÇ´Â ÀÌÀ¯ ¼³¸í¾Ë¶÷
            Managers.UI.ShowPopupUI<UI_AlertPopup>().SetInfo(201);
            _inputField.text = "";
        }
    }

    public void ConfirmNickName()
    {
        Managers.Game.Name = _inputField.text;
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
        //Play Popup ¶ç¿ì±â
    }
}
