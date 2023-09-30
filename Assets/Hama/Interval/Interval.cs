using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interval : MonoBehaviour
{
    [SerializeField] Text turnCount;
    void Start()
    {
        //プレイヤー変更
        MainGameProgress.gameStaus = MainGameProgress.GameStaus.Wait;
        turnCount.text = "経過ターン　"+MainGameManager.nowTurn.ToString()+"/16";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MainGameSceneChange()
    {
        if(SceneManager.OldSceneName == SceneManager.SceneName.MAINGAME_A.ToString().ToLower())
        {
            SceneManager.SceneLaod(SceneManager.SceneName.MAINGAME_B);
        }
        else if (SceneManager.OldSceneName == SceneManager.SceneName.MAINGAME_B.ToString().ToLower())
        {
            SceneManager.SceneLaod(SceneManager.SceneName.MAINGAME_A);
        }
    }

    public void GameStausWaitToPlayerTurnChange()
    {
        if(MainGameProgress.gameStaus == MainGameProgress.GameStaus.Wait)
            MainGameProgress.gameStaus = MainGameProgress.GameStaus.PlayerTurn;
    }
}
