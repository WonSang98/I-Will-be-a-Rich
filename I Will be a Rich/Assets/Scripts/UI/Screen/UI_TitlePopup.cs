using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TitlePopup : UI_Popup
{
    enum Texts
    {
        TouchToStartText,
        TitleText
    }

    enum Buttons
    {
        Start_Button,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("UI_TitlePopup Init");

        //UI_BASE�� enum �̸����� object ã�Ƽ� ���ε�
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));


        //���ε��Ǿ�������, Enum Value�� ������Ʈ�� ã�Ƽ� �̺�Ʈ�� �ؽ�Ʈ ��Ī
        //TODO : ��ư �̺�Ʈ ���ε�
        GetButton((int)Buttons.Start_Button).gameObject.BindEvent(OnClickStartButton);

        //TODO : �ؽ�Ʈ ���ε�
        GetText((int)Texts.TitleText).text = Managers.GetText(10000);
        GetText((int)Texts.TouchToStartText).text = Managers.GetText(10001);


        //TODO : ���� �۾�


        return true;
    }

    void OnClickStartButton()
    {
        Debug.Log("OnClickStartButton");
        //TODO : SFX

        //TODO : ������ Ȯ��
        if (Managers.Game.LoadGame())
        {
            Managers.UI.ClosePopupUI(this);
            Managers.UI.ShowPopupUI<UI_JoinNewUser>();
            //Managers.UI.ShowPopupUI<UI_PlayPopup>(); <- ���� ����� ����
        }
        else
        {
            Managers.Game.Init();
            Managers.Game.SaveGame();

            Managers.UI.ClosePopupUI(this);
            Managers.UI.ShowPopupUI<UI_JoinNewUser>();
        }

    }
}
