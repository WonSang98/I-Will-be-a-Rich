using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Log : UI_Scene
{
    enum Texts
    {
        Log00_Text,
        Log01_Text,
        Log02_Text,
        Log03_Text,
        Log04_Text,
        Log05_Text,
        Log06_Text
    }

    Action _earningAction;
    Queue<int> _earningQueue = new Queue<int>();

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("UI_Log Init");
        BindText(typeof(Texts));
        LogsInit();
        _earningAction -= LogRefresh;
        _earningAction += LogRefresh;
        return true;
    }

    void LogsInit()
    {
        for(int i=0; i<=(int)Texts.Log06_Text; i++)
        {
            _earningQueue.Enqueue(0);
            GetText(i).text = "";
        }
    }

    void LogRefresh()
    {
        int idx = 0;
        foreach(int earn in _earningQueue)
        {
            GetText(idx++).text = $"+ {earn}";
        }
    }

    public void InterNewLog(int newData)
    {
        int tmp;
        _earningQueue.TryDequeue(out tmp);
        _earningQueue.Enqueue(newData);
        _earningAction?.Invoke();
    }

}
