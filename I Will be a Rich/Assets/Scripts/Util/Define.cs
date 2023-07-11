using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }

    public enum Scene
    {
        Unknown,
        Dev,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
    }

    public enum StatType
    {
        work,
        upgrade00,
        upgrade01,
        upgrade02,
        upgrade03
    }

    public enum LottoType
    {
        lotto00,
        lotto01,
        lotto02,
        lotto03,
        lotto04,
        lotto05,
        lotto06,
        lotto07
    }

    
    #region �÷��̿�� ����
    public const int MAX_LOTTO_COUNT = 10;
    public const int MAX_UPGRADE_COUNT = 4;
    public const int LOTTO_PRIZE_COUNT = 6;
    #endregion

    #region ��ȭ
    public const float START_SECOND_PER_CHAR = 0.05f;
    #endregion

    #region STAT �ڵ�
    public const int WORK = 10000;
    public const int UPGRADE00 = 30000;
    public const int UPGRADE01 = 30001;
    public const int UPGRADE02 = 30002;
    public const int UPGRADE03 = 30003;
    #endregion

    #region LOTTO
    public const int LOTTO_DEFAULT_CODE = 20000;
    public const int PRIZE1 = 8145060;//8145060;
    public const int PRIZE2 = 7145060;//8145053;
    public const int PRIZE3 = 5145060;//8144825;
    public const int PRIZE4 = 1145060;//8133713;
    public const int PRIZE5 = 145060; //7952712;
    #endregion

}
