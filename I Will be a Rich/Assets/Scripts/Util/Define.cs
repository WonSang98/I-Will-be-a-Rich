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

    
    #region 플레이요소 개수
    public const int MAX_LOTTO_COUNT = 10;
    public const int MAX_UPGRADE_COUNT = 4;
    public const int LOTTO_PRIZE_COUNT = 6;
    #endregion

    #region 대화
    public const float START_SECOND_PER_CHAR = 0.05f;
    #endregion

    #region STAT 코드
    public const int WORK = 10000;
    public const int UPGRADE00 = 30000;
    public const int UPGRADE01 = 30001;
    public const int UPGRADE02 = 30002;
    public const int UPGRADE03 = 30003;
    #endregion

    #region LOTTO
    public const int LOTTO_DEFAULT_CODE = 20000;
    #endregion

}
