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

    public const int MAX_LOTTO_COUNT = 10;
    public const int MAX_UPGRADE_COUNT = 4;



}
