using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LogInfo
{
    public string _where;
    public int _earn;

    public LogInfo(string where, int earn)
    {
        _where = where;
        _earn = earn;
    }
}
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
    Queue<LogInfo> _earningQueue = new Queue<LogInfo>();

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
            GetText(i).text = "";
        }
    }

    void LogRefresh()
    {
        int idx = 0;
        foreach(LogInfo earn in _earningQueue)
        {
            GetText(idx++).text = $"{earn._where} +{earn._earn}";
        }
    }

    public void InterNewLog(LogInfo newData)
    {
        if(_earningQueue.Count > (int)Texts.Log06_Text+1) _earningQueue.TryDequeue(out LogInfo tmp);
        _earningQueue.Enqueue(newData);
        _earningAction?.Invoke();
    }

}
