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

        //UI_BASE에 enum 이름으로 object 찾아서 바인딩
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));


        //바인딩되어있으니, Enum Value로 오브젝트를 찾아서 이벤트나 텍스트 매칭
        //TODO : 버튼 이벤트 바인딩
        GetButton((int)Buttons.Start_Button).gameObject.BindEvent(OnClickStartButton);

        //TODO : 텍스트 바인딩
        GetText((int)Texts.TitleText).text = Managers.GetText(10000);
        GetText((int)Texts.TouchToStartText).text = Managers.GetText(10001);


        //TODO : 사운드 작업


        return true;
    }

    void OnClickStartButton()
    {
        Debug.Log("OnClickStartButton");
        //TODO : SFX

        //TODO : 데이터 확인
        if (Managers.Game.LoadGame())
        {
            Managers.UI.ClosePopupUI(this);
            Managers.UI.ShowPopupUI<UI_JoinNewUser>();
            //Managers.UI.ShowPopupUI<UI_PlayPopup>(); <- 추후 만들고 해제
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
