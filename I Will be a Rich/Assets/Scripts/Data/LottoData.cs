using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static Define;

public class LottoData
{
    [XmlAttribute]
    public int ID;
    [XmlAttribute]
    public LottoType type;
    [XmlAttribute]
    public int nameID;
    [XmlAttribute]
    public int price;
    [XmlAttribute]
    public float coolTime;
    [XmlAttribute]
    public int max;
    [XmlAttribute]
    public int prize1;
    [XmlAttribute]
    public int prize2;
    [XmlAttribute]
    public int prize3;
    [XmlAttribute]
    public int prize4;
    [XmlAttribute]
    public int prize5;
}

[Serializable, XmlRoot("ArrayOfStatData")]
public class LottoDataLoader : ILoader<int, LottoData>
{
    [XmlElement("LottoData")]
    public List<LottoData> _lottoData = new List<LottoData>();

    public Dictionary<int, LottoData> MakeDic()
    {
        Dictionary<int, LottoData> dic = new Dictionary<int, LottoData>();

        foreach (LottoData data in _lottoData)
            dic.Add(data.ID, data);

        return dic;
    }

    public bool Validate()
    {
        return true;
    }
}