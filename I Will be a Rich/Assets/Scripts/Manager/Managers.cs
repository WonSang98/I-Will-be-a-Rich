using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instance = null;
    public static Managers Instance { get { return s_instance; } }

    //유일성
    private static ResourceManager s_resourceManager = new ResourceManager();
    private static UIManager s_uiManager = new UIManager();
    private static DataManager s_dataManager = new DataManager();

    //가져오기
    public static ResourceManager Resource { get { Init(); return s_resourceManager; } }
    public static UIManager UI { get { Init(); return s_uiManager; } }
    public static DataManager Data { get { Init(); return s_dataManager; } }
    private void Start()
    {
        Init();
    }

    public static string GetText(int id)
    {
        if (Managers.Data.Texts.TryGetValue(id, out TextData value) == false)
            return "";

        //TODO : 지역별 출력 변경
        //USER LANGUAGE에 따라서...
        return value.kor;
    }

    private static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
            }

            s_instance = Utils.GetOrAddComponent<Managers>(go);
            s_resourceManager.Init();
            s_dataManager.Init();
            DontDestroyOnLoad(go);

            Application.targetFrameRate = 60;
        }
    }
}
