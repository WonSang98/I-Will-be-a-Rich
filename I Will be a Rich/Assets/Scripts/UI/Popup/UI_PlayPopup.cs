using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayPopup : UI_Popup
{
    #region ENUM
    enum Texts
    {
        Name_Text, // 플레이어 이름
        hasMoney_Text, // 소지 금액
        balance_Text, // KOR = 잔고 / ENG = BALANCE
        Point_Text, // 환생 포인트
    }

    enum Buttons
    {
        Button_Work,
        Button_Lotto,
        Button_Upgrade,
        Button_Event,
        Button_Option,
        Touch_Button,
    }

    enum Images
    {
        WorkName_Image,
        LottoName_Image,
        UpgradeName_Image,
        EventName_Image,
        OptionName_Image
    }

    enum GameObjects
    {
        UI_Log,
        WorkTab,
        WorkContent,
        LottoTab,
        LottoContent,
        UpgradeTab,
        UpgradeContent,
        EventTab,
        EventContent,
        OptionTab,
        OptionContent,
    }

    public enum PlayTab
    {
        None,
        Work,
        Lotto,
        Upgrade,
        Event,
        Option
    }

    enum LottoItems
    {
        UI_LottoItem_00,
        UI_LottoItem_01,
        UI_LottoItem_02,
        UI_LottoItem_03,
        UI_LottoItem_04,
        UI_LottoItem_05,
        UI_LottoItem_06,
        UI_LottoItem_07,
    }

    enum OptionItems
    {
        UI_BGMController,
        UI_VFXController,
        UI_Language,
    }
    #endregion

    PlayTab _tab = PlayTab.None;
    UI_Log _showLog;
    float[] _lootoCoolTime = new float[Define.MAX_LOTTO_COUNT];
    Coroutine[] _coLotto = new Coroutine[Define.MAX_LOTTO_COUNT];
    StatData _workdata;

    private void Update()
    {
        RefreshHUD();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("UI_PlayPopup Init");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        Bind<UI_LottoItem>(typeof(LottoItems));

        //Set Text
        GetText((int)Texts.Name_Text).text = Managers.GetText(40000);
        GetText((int)Texts.balance_Text).text = Managers.GetText(40001);

        //Set Button
        GetButton((int)Buttons.Touch_Button).gameObject.BindEvent(OnTouchEvent);
        GetButton((int)Buttons.Button_Work).gameObject.BindEvent(() => ShowTap(PlayTab.Work));
        GetButton((int)Buttons.Button_Lotto).gameObject.BindEvent(() => ShowTap(PlayTab.Lotto));
        GetButton((int)Buttons.Button_Upgrade).gameObject.BindEvent(() => ShowTap(PlayTab.Upgrade));
        GetButton((int)Buttons.Button_Event).gameObject.BindEvent(() => ShowTap(PlayTab.Event));
        GetButton((int)Buttons.Button_Option).gameObject.BindEvent(() => ShowTap(PlayTab.Option));


        //TAB
        ShowTap(PlayTab.Work);
        _showLog = GetObject((int)GameObjects.UI_Log).gameObject.GetComponent<UI_Log>();


        //LOTTOS
        InitLottoItem();
        _lootoCoolTime = Managers.Game.LottoCoolTimes;

        //STATS
        Managers.Data.Stats.TryGetValue((int)Define.StatType.work + 1, out _workdata);

        for (int i=0; i<Define.MAX_LOTTO_COUNT; i++)
        {
            _coLotto[i] = StartCoroutine(LottoCoolingDown(i));
        }
        
        return true;
    }


    void OnTouchEvent()
    {
        float mul = _workdata.increaseValue == 1 ? Managers.Game.WorkAbility + 1 : Mathf.Pow(_workdata.increaseValue, (Managers.Game.WorkAbility + 1));
        Managers.Game.Money += (int)(_workdata.value * mul);

    }

    void InitLottoItem()
    {
        for(int i=0; i< System.Enum.GetValues(typeof(LottoItems)).Length; i++)
        {
            SetLottoProbability(i);
            Get<UI_LottoItem>(i).SetInfo(i);
        }
    }

    void ShowTap(PlayTab tab)
    {
        if (_tab == tab) return;

        _tab = tab;
        GetObject((int)GameObjects.WorkTab).gameObject.SetActive(false);
        GetObject((int)GameObjects.LottoTab).gameObject.SetActive(false);
        GetObject((int)GameObjects.UpgradeTab).gameObject.SetActive(false);
        GetObject((int)GameObjects.EventTab).gameObject.SetActive(false);
        GetObject((int)GameObjects.OptionTab).gameObject.SetActive(false);
        GetButton((int)Buttons.Button_Work).image.sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_05");
        GetButton((int)Buttons.Button_Lotto).image.sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_06");
        GetButton((int)Buttons.Button_Upgrade).image.sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_07");
        GetButton((int)Buttons.Button_Event).image.sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_08");
        GetImage((int)Images.WorkName_Image).sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_04");
        GetImage((int)Images.LottoName_Image).sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_04");
        GetImage((int)Images.UpgradeName_Image).sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_04");
        GetImage((int)Images.EventName_Image).sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_04");

        switch (_tab)
        {
            case PlayTab.Work:
                //TODO SOUND
                GetObject((int)GameObjects.WorkTab).gameObject.SetActive(true);
                GetObject((int)GameObjects.WorkTab).GetComponentInChildren<ScrollRect>().ResetVertical();
                GetButton((int)Buttons.Button_Work).image.sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_18");
                GetImage((int)Images.WorkName_Image).sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_12");
                break;
            case PlayTab.Lotto:
                GetObject((int)GameObjects.LottoTab).gameObject.SetActive(true);
                GetObject((int)GameObjects.LottoTab).GetComponentInChildren<ScrollRect>().ResetVertical();
                GetButton((int)Buttons.Button_Lotto).image.sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_19");
                GetImage((int)Images.LottoName_Image).sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_12");
                break;
            case PlayTab.Upgrade:
                GetObject((int)GameObjects.UpgradeTab).gameObject.SetActive(true);
                GetObject((int)GameObjects.UpgradeTab).GetComponentInChildren<ScrollRect>().ResetVertical();
                GetButton((int)Buttons.Button_Upgrade).image.sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_20");
                GetImage((int)Images.UpgradeName_Image).sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_12");
                break;
            case PlayTab.Event:
                GetObject((int)GameObjects.EventTab).gameObject.SetActive(true);
                GetObject((int)GameObjects.EventTab).GetComponentInChildren<ScrollRect>().ResetVertical();
                GetButton((int)Buttons.Button_Event).image.sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_21");
                GetImage((int)Images.EventName_Image).sprite = Managers.Resource.Load<Sprite>("Sprites/AntCompnay_UI/btn_12");
                break;
            case PlayTab.Option:
                GetObject((int)GameObjects.OptionTab).gameObject.SetActive(true);
                GetObject((int)GameObjects.OptionTab).GetComponentInChildren<ScrollRect>().ResetVertical();
                break;

        }
    }

    public IEnumerator LottoCoolingDown(int lottoType)
    {
        Managers.Data.Lottos.TryGetValue((int)lottoType + 1, out LottoData data);
        int prize = 0;
        int rank = 0;
        while (true)
        {
            while (Managers.Game.LottoCounts[(int)lottoType] > 0)
            {
                Debug.Log($"{lottoType}  is COOLIing...");

                float _time = data.coolTime;
                Managers.Game.LottoCoolTimes[lottoType] = _time;
                while (Managers.Game.LottoCoolTimes[lottoType] >= 0)
                {
                    Managers.Game.LottoCoolTimes[lottoType] -= 0.1f;
                    Get<UI_LottoItem>(lottoType).RefreshTime();
                    yield return new WaitForSeconds(0.1f);
                }

                int _rand = Random.Range(1, Managers.Game.LottoProbability[(int)lottoType, 0]+1);
                Debug.Log("내가 뽑은 수는! : " + _rand);
                Managers.Game.LottoCounts[(int)lottoType] -= 1;
                Get<UI_LottoItem>(lottoType).RefreshCount();
                if (_rand < Managers.Game.LottoProbability[(int)lottoType, 4])
                {
                    continue;
                }
                else if (_rand < Managers.Game.LottoProbability[(int)lottoType, 3])
                {
                    //5등
                    rank = 5;
                    prize = data.prize5;
                }
                else if (_rand < Managers.Game.LottoProbability[(int)lottoType, 2])
                {
                    //4등
                    rank = 4;
                    prize = data.prize4;
                }
                else if (_rand < Managers.Game.LottoProbability[(int)lottoType, 1])
                {
                    //3등
                    rank = 3;
                    prize = data.prize3;
                }
                else if (_rand < Managers.Game.LottoProbability[(int)lottoType, 0])
                {
                    //2등
                    rank = 2;
                    prize = data.prize2;
                }
                else
                {
                    //1등
                    rank = 1;
                    prize = data.prize1;
                }
                Managers.Game.Money += prize;

                if (prize != 0)
                {
                    LogInfo _log = new LogInfo($"로또{lottoType} - {rank}등 당첨!", prize) ;
                    _showLog.InterNewLog(_log);
                }

            }
            yield return null;
        }

    }

    void SetLottoProbability(int lottoType)
    {
        Managers.Game.LottoProbability[lottoType, 0] = Define.PRIZE1;
        Managers.Game.LottoProbability[lottoType, 1] = Define.PRIZE2;
        Managers.Game.LottoProbability[lottoType, 2] = Define.PRIZE3;
        Managers.Game.LottoProbability[lottoType, 3] = Define.PRIZE4;
        Managers.Game.LottoProbability[lottoType, 4] = Define.PRIZE5;
    }

    #region Refresh
    void RefreshHUD()
    {
        //소지 금액, 포인트 새로고침
        GetText((int)Texts.hasMoney_Text).text = Managers.Game.Money.ToString() + "\\";
        GetText((int)Texts.Point_Text).text = Managers.Game.Point.ToString() + 'P';
    }

    #endregion
}
