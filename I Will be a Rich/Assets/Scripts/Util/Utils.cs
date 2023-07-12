using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null) component = go.AddComponent<T>();
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T :UnityEngine.Object
    {
        if (go == null) return null;

        if(recursive == false)
        {
            Transform transform = go.transform.Find(name);
            if (transform != null) return transform.GetComponent<T>();
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }

    public static void BuyItem(int cost, Action buyAction, Define.CostType type = Define.CostType.Money)
    {
        switch(type)
        {
            case Define.CostType.Money:
                if (Managers.Game.Money >= cost)
                {
                    Managers.Game.Money -= cost;
                    buyAction?.Invoke();
                    Managers.Game.SaveGame();
                }
                else
                {
                    Debug.Log("Failed BuyItem");
                    //TODO SHOW ALERT POPUP;
                }
                break;
            case Define.CostType.Point:
                if (Managers.Game.Point >= cost)
                {
                    Managers.Game.Point -= cost;
                    buyAction?.Invoke();
                    Managers.Game.SaveGame();
                }
                else
                {
                    Debug.Log("Failed BuyItem");
                    //TODO SHOW ALERT POPUP;
                }
                break;
        }



    }

}
