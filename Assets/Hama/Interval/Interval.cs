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
        //プレイヤー変更
        MainGameProgress.gameStaus = MainGameProgress.GameStaus.Wait;
        turnCount.text = "経過ターン　" + MainGameManager.nowTurn.ToString() + "/16";
    }
    public void OnClickAdButton()
    {
        admobReward.ShowAdMobReward();
        shareTextObj.SetActive(false);
        adButton.SetActive(false);
        startTurnButton.SetActive(true);
        preparationTextObj.SetActive(true);
    }
    public void OnClickStartTurnButton()
    {
        MainGameSceneChange();
        ChangeGameStausWaitToPlayerTurn();
    }
    void MainGameSceneChange()
    {
        if (SceneManager.OldSceneName == SceneManager.SceneName.MAINGAME_A.ToString().ToLower())
        {
            SceneManager.SceneLaod(SceneManager.SceneName.MAINGAME_B);
        }
        else if (SceneManager.OldSceneName == SceneManager.SceneName.MAINGAME_B.ToString().ToLower())
        {
            SceneManager.SceneLaod(SceneManager.SceneName.MAINGAME_A);
        }
    }

    void ChangeGameStausWaitToPlayerTurn()
    {
        if (MainGameProgress.gameStaus == MainGameProgress.GameStaus.Wait)
            MainGameProgress.gameStaus = MainGameProgress.GameStaus.PlayerTurn;
    }
}
