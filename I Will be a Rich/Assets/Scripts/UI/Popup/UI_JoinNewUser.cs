using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JoinNewUser : UI_Popup
{
    enum Texts
    {
        SignUp_Text,
    }

    enum Buttons
    {
        Next_Button,
    }

    int _talkIndex = 0;
    const int TOTAL_TALK = 5;

    string _text;
    int _charIndex = 0;
    float _secondPerCharacter = Define.START_SECOND_PER_CHAR;


    Action _onTextEndCallback;
    Coroutine _coShowText;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("UI_JoinNewUser Init");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Next_Button).gameObject.BindEvent(OnClickNextButton);

        _talkIndex = 0;
        ShowText();

        _onTextEndCallback -= ClearCoroutine;
        _onTextEndCallback += ClearCoroutine;
        

        return true;
    }

    void OnClickNextButton()
    {
        if(_talkIndex < TOTAL_TALK )
        {
            ShowText();
        }
        else
        {
            if(_coShowText == null)
            {
                //다음 씬 전환
                Debug.Log("End Join New User");
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_InputNamePopup>();
            }
        }
    }

    void ShowText()
    {
        if (_coShowText == null)
        {
            //TODO 사운드 출력
            _text = Managers.GetText(20000 + _talkIndex++);
            _charIndex = 0;
            _secondPerCharacter = Define.START_SECOND_PER_CHAR;
            _coShowText = StartCoroutine(CoShowText());
        }
    }

    IEnumerator CoShowText()
    {
        
        while(true)
        {
            if(_charIndex >= _text.Length)
            {
                //지문이 다 나온 경우
                GetText((int)Texts.SignUp_Text).text = _text;
                //TODO : SOUND 종료
                yield return new WaitForSeconds(0.5f);

                //지문이 다 나왔음을 알려줌.
                _onTextEndCallback?.Invoke();
                break;
            }

            _charIndex++;
            string text = _text.Substring(0, _charIndex);
            GetText((int)Texts.SignUp_Text).text = text;

            yield return new WaitForSeconds(_secondPerCharacter);
        }
    }

    void ClearCoroutine()
    {
        StopCoroutine(_coShowText);
        _coShowText = null;
    }
}
