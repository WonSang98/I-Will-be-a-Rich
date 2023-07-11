using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public bool isEvent = false;
}

[Serializable]
public class GameData
{
	public string Name;

	public int WorkAbility; // 화면 터치
	public int[] LottoAbilities = new int[Define.MAX_LOTTO_COUNT];
	public float[] LottoCoolTimes = new float[Define.MAX_LOTTO_COUNT];
	public int[,] LottoProbability = new int[Define.MAX_LOTTO_COUNT, Define.LOTTO_PRIZE_COUNT];
    public int[] Upgrades = new int[Define.MAX_UPGRADE_COUNT];
	public PlayerState Player;

	public int Money;
	public int[] LottoCounts = new int[Define.MAX_LOTTO_COUNT];
	public int Point;
    public int RePlay;

	public float PlayTime;


	
}
public class GameManagerEx : MonoBehaviour
{
	GameData _gameData = new GameData();

    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    #region 스탯
	public string Name
    {
        get { return _gameData.Name; }
		set { _gameData.Name = value; }
    }

	public int WorkAbility
    {
		get { return _gameData.WorkAbility;}
		set { _gameData.WorkAbility = value; }
    }

	public int[] LottoAbilities
    {
		get { return _gameData.LottoAbilities; }
		set { _gameData.LottoAbilities = value; }
    }

	public float[] LottoCoolTimes
	{
		get { return _gameData.LottoCoolTimes; }
		set { _gameData.LottoCoolTimes = value; }
	}

	public int[,] LottoProbability
    {
		get { return _gameData.LottoProbability; }
        set { _gameData.LottoProbability = value; }
    }

	public int[] Upgrades
	{
		get { return _gameData.Upgrades; }
		set { _gameData.Upgrades = value; }
	}

	public PlayerState Player
    {
		get { return _gameData.Player; }
		set { _gameData.Player = value; }
    }
	#endregion

	#region 재화

	public int Money
    {
        get { return _gameData.Money; }
		set { _gameData.Money = value; }
    }
	public int[] LottoCounts
	{
		get { return _gameData.LottoCounts; }
		set { _gameData.LottoCounts = value; }
	}

	public int Point
	{
		get { return _gameData.Point; }
		set { _gameData.Point = value; }
	}
	public int RePlay
	{
		get { return _gameData.RePlay; }
		set { _gameData.RePlay = value; }
	}

	#endregion

	#region 플레이정보
	public float PlayTime
	{
		get { return _gameData.PlayTime; }
		set { _gameData.PlayTime = value; }
	}

	#endregion

	public void Init()
	{
		Name = "NoName";

		WorkAbility = 1; // 화면 터치

		for(int i=0; i<Define.MAX_LOTTO_COUNT; i++)
        {
			LottoAbilities[i] = 0;
			LottoCounts[i] = 0;
        }

		for(int i=0; i<Define.MAX_UPGRADE_COUNT; i++)
        {
			Upgrades[i] = 0;
        }

		Player = new PlayerState();

		Money = 0;
		Point = 0;
		RePlay = 0;

		PlayTime = 0.0f;
}

	#region Save & Load	
	public string _path = Application.persistentDataPath + "/SaveData.json";

	public void SaveGame()
	{
		string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
		File.WriteAllText(_path, jsonStr);
		Debug.Log($"Save Game Completed : {_path}");
	}

	public bool LoadGame()
	{
		if (File.Exists(_path) == false)
			return false;

		string fileStr = File.ReadAllText(_path);
		GameData data = JsonUtility.FromJson<GameData>(fileStr);
		if (data != null)
		{
			Managers.Game.SaveData = data;
		}

		Debug.Log($"Save Game Loaded : {_path}");
		return true;
	}
	#endregion
}
