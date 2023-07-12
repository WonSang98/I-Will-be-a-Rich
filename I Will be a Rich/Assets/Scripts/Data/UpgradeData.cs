using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static Define;

public class UpgradeData
{
    [XmlAttribute]
    public int ID;
    [XmlAttribute]
    public Upgrade type;
    [XmlAttribute]
    public int nameID;
    [XmlAttribute]
    public int price;
    [XmlAttribute]
    public float increasePrice;
    [XmlAttribute]
    public float value;
    [XmlAttribute]
    public float increaseValue;
    [XmlAttribute]
    public int max;
}

[Serializable, XmlRoot("ArrayOfStatData")]
public class UpgradeDataLoader : ILoader<int, UpgradeData>
{
    [XmlElement("UpgradeData")]
    public List<UpgradeData> _upgradeData = new List<UpgradeData>();

    public Dictionary<int, UpgradeData> MakeDic()
    {
        Dictionary<int, UpgradeData> dic = new Dictionary<int, UpgradeData>();

        foreach (UpgradeData data in _upgradeData)
            dic.Add(data.ID, data);

        return dic;
    }

    public bool Validate()
    {
        return true;
    }
}