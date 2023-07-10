using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayPopup : MonoBehaviour
{
    #region ENUM
    enum Texts
    {
        Name_Text, // �÷��̾� �̸�
        hasMoney_Text, // ���� �ݾ�
        balance_Text, // KOR = �ܰ� / ENG = BALANCE
        Point_Text, // ȯ�� ����Ʈ
    }

    enum Buttons
    {
        Button_Work,
        Button_Lotto,
        Button_Upgrade,
        Button_Event,
        Button_Option,
    }

    enum GameObjects
    {
        UI_Log,
        WorkTab,
        WorkContent,
        LottoTab,
        LottoContent,
        UpgradeTab,
        UpgradeContent,
        EventTab,
        EventContent,
        OptionTab,
        OptionContent,
    }

    public enum PlayTab
    {
        None,
        Work,
        Lotto,
        Upgrade,
        Event,
        Option
    }

    enum OptionItems
    {
        UI_BGMController,
        UI_VFXController,
        UI_Language,
    }
    #endregion

    
}
