using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Interval :MonoBehaviour
{
    public static Interval instance;
    [SerializeField] GameObject shareTextObj;
    [SerializeField] GameObject adButton;
    [SerializeField] GameObject startTurnButton;
    [SerializeField] GameObject preparationTextObj;
    [SerializeField] AdMobReward admobReward;
    [SerializeField] Sprite[] spriteTurn = new Sprite[16];
    [SerializeField] GameObject TurnCountObj;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TurnManager.TurnCountUp();

        if (TurnCountObj == null)
        {
            Debug.LogWarning("TurnCountObjが設定されていません");
            return;
        }

        TurnCountObj.GetComponent<Image>().sprite = spriteTurn[TurnManager.nowTurn - 1];
    }

    /// <summary>
    /// アンドロイドリワード広告開始ボタン
    /// </summary>
    public void OnClickAdRewardButton()
    {
        admobReward.ShowAdMobReward();
        shareTextObj.SetActive(false);
        adButton.SetActive(false);
        startTurnButton.SetActive(true);
        preparationTextObj.SetActive(true);
    }

    /// <summary>
    /// ターン開始ボタン
    /// </summary>
    public void OnClickStartTurnButton()
    {
        MainGameSceneChange();
        ChangeGameStausWaitToPlayerTurn();
    }

    /// <summary>
    /// 前半ターンと後半ターンの切り替え遷移
    /// </summary>
    void MainGameSceneChange()
    {
        if (SceneTransitions.OldSceneName == SceneTransitions.SceneName.MAINGAMEFIRST.ToString().ToLower())
        {
            SceneTransitions.SceneLaod(SceneTransitions.SceneName.MAINGAMELAST);
        }
        else if (SceneTransitions.OldSceneName == SceneTransitions.SceneName.MAINGAMELAST.ToString().ToLower())
        {
            SceneTransitions.SceneLaod(SceneTransitions.SceneName.MAINGAMEFIRST);
        }
    }

    /// <summary>
    /// ゲームステータス待ちからプレイヤーターンに遷移
    /// </summary>
    void ChangeGameStausWaitToPlayerTurn()
    {
        if (MainGameProgress.gameStaus == MainGameProgress.GameStaus.IntervalEnd)
            MainGameProgress.gameStaus = MainGameProgress.GameStaus.ResetTurn;
    }
}
