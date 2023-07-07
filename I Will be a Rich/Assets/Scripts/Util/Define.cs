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
    #region 플레이요소 개수
    public const int MAX_LOTTO_COUNT = 10;
    public const int MAX_UPGRADE_COUNT = 4;
    #endregion

    #region 대화
    public const float START_SECOND_PER_CHAR = 0.05f;
    #endregion


}
