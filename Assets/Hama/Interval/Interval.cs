using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interval : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー変更
        MainGameProgress.gameStaus = MainGameProgress.GameStaus.Wait;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MainGameProgress.gameStaus);
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
