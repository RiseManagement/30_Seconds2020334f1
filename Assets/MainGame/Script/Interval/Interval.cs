using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Interval :MonoBehaviour
{
    public static Interval instance;
    [SerializeField] Text turnCount;
    [SerializeField] GameObject shareTextObj;
    [SerializeField] GameObject adButton;
    [SerializeField] GameObject startTurnButton;
    [SerializeField] GameObject preparationTextObj;
    [SerializeField] AdMobReward admobReward;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        MainGameManager.TurnCountUp();
        //プレイヤー変更
        turnCount.text = "経過ターン　" + MainGameManager.nowTurn.ToString() + "/16";
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
        if (MainGameProgress.gameStaus == MainGameProgress.GameStaus.Interval)
            MainGameProgress.gameStaus = MainGameProgress.GameStaus.ResetTurn;
    }
}
