using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using static Define;
using System.Linq;

public class DataTransformer : EditorWindow
{
	[MenuItem("Tools/RemoveSaveData")]
	public static void RemoveSaveData()
	{
		string path = Application.persistentDataPath + "/SaveData.json";
		if (File.Exists(path))
		{
			File.Delete(path);
			Debug.Log("SaveFile Deleted");
		}
		else
		{
			Debug.Log("No SaveFile Detected");
		}
	}

	[MenuItem("Tools/ParseExcel")]
	public static void ParseExcel()
	{
		ParseTextData();
		ParseLottoData();
		ParseStatData();
		ParseUpgradeData();
	}


	static void ParseTextData()
	{
		List<TextData> textDatas = new List<TextData>();

		#region ExcelData
		string[] lines = Resources.Load<TextAsset>($"Data/Excel/TextData").text.Split("\n");

		// 첫번째 라인까지 스킵
		for (int y = 1; y < lines.Length; y++)
		{
			string[] row = lines[y].Replace("\r", "").Split(',');
			if (row.Length == 0)
				continue;
			if (string.IsNullOrEmpty(row[0]))
				continue;

			textDatas.Add(new TextData()
			{
				ID = int.Parse(row[0]),
				kor = row[1],
				eng = row[2]
			});
		}
		#endregion

		string xmlString = ToXML(new TextDataLoader() { _textData = textDatas });
		File.WriteAllText($"{Application.dataPath}/Resources/Data/TextData.xml", xmlString);
		AssetDatabase.Refresh();
	}

	static void ParseLottoData()
	{
		List<LottoData> lottoDatas = new List<LottoData>();

		#region ExcelData
		string[] lines = Resources.Load<TextAsset>($"Data/Excel/LottoData").text.Split("\n");

		// 두번째 라인까지 스킵
		for (int y = 2; y < lines.Length; y++)
		{
			string[] row = lines[y].Replace("\r", "").Split(',');
			if (row.Length == 0)
				continue;
			if (string.IsNullOrEmpty(row[0]))
				continue;

			lottoDatas.Add(new LottoData()
			{
				ID = int.Parse(row[0]),
				type = (LottoType)LottoType.Parse(typeof(LottoType), row[1]),
				nameID = int.Parse(row[2]),
				price = int.Parse(row[3]),
				coolTime = float.Parse(row[3]),
				max = int.Parse(row[4]),
				prize1 = int.Parse(row[5]),
				prize2 = int.Parse(row[6]),
				prize3 = int.Parse(row[7]),
				prize4 = int.Parse(row[8]),
				prize5 = int.Parse(row[9])
			});
		}
		#endregion

		string xmlString = ToXML(new LottoDataLoader() { _lottoData = lottoDatas });
		File.WriteAllText($"{Application.dataPath}/Resources/Data/LottoData.xml", xmlString);
		AssetDatabase.Refresh();
	}

	static void ParseStatData()
	{
		List<StatData> statDatas = new List<StatData>();

		#region ExcelData
		string[] lines = Resources.Load<TextAsset>($"Data/Excel/StatData").text.Split("\n");

		// 첫번째 라인까지 스킵
		for (int y = 2; y < lines.Length; y++)
		{
			string[] row = lines[y].Replace("\r", "").Split(',');
			if (row.Length == 0)
				continue;
			if (string.IsNullOrEmpty(row[0]))
				continue;

			statDatas.Add(new StatData()
			{
				ID = int.Parse(row[0]),
				type = (StatType)StatType.Parse(typeof(StatType), row[1]),
				nameID = int.Parse(row[2]),
				price = int.Parse(row[3]),
				increasePrice = int.Parse(row[4]),
				value = int.Parse(row[5]),
				increaseValue = float.Parse(row[6]),
				maxLevel = int.Parse(row[7]),
			});
		}
		#endregion

		string xmlString = ToXML(new StatDataLoader() { _statData = statDatas });
		File.WriteAllText($"{Application.dataPath}/Resources/Data/StatData.xml", xmlString);
		AssetDatabase.Refresh();
	}

	static void ParseUpgradeData()
	{
		List<UpgradeData> upgradeData = new List<UpgradeData>();

		#region ExcelData
		string[] lines = Resources.Load<TextAsset>($"Data/Excel/UpgradeData").text.Split("\n");

		// 첫번째 라인까지 스킵
		for (int y = 2; y < lines.Length; y++)
		{
			string[] row = lines[y].Replace("\r", "").Split(',');
			if (row.Length == 0)
				continue;
			if (string.IsNullOrEmpty(row[0]))
				continue;

			upgradeData.Add(new UpgradeData()
			{
				ID = int.Parse(row[0]),
				type = (Upgrade)Upgrade.Parse(typeof(Upgrade), row[1]),
				nameID = int.Parse(row[2]),
				price = int.Parse(row[3]),
				increasePrice = float.Parse(row[4]),
				value = float.Parse(row[5]),
				increaseValue = float.Parse(row[6]),
				max = int.Parse(row[7]),
			});
		}
		#endregion

		string xmlString = ToXML(new UpgradeDataLoader() { _upgradeData = upgradeData });
		File.WriteAllText($"{Application.dataPath}/Resources/Data/UpgradeData.xml", xmlString);
		AssetDatabase.Refresh();
	}


	#region XML 유틸
	public sealed class ExtentedStringWriter : StringWriter
	{
		private readonly Encoding stringWriterEncoding;

		public ExtentedStringWriter(StringBuilder builder, Encoding desiredEncoding) : base(builder)
		{
			this.stringWriterEncoding = desiredEncoding;
		}

		public override Encoding Encoding
		{
			get
			{
				return this.stringWriterEncoding;
			}
		}
	}

	public static string ToXML<T>(T obj)
	{
		using (ExtentedStringWriter stringWriter = new ExtentedStringWriter(new StringBuilder(), Encoding.UTF8))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			xmlSerializer.Serialize(stringWriter, obj);
			return stringWriter.ToString();
		}
	}
	#endregion
}
