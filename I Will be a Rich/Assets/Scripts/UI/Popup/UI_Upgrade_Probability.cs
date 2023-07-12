using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Upgrade_Probability : UI_Upgrade
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    protected override void SetType()
    {
        base.SetType();
        _type = Define.Upgrade.Probability;
    }
}
