using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : UI_Base
{
	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		Managers.UI.SetCanvas(gameObject, false);
		return true;
	}
}
