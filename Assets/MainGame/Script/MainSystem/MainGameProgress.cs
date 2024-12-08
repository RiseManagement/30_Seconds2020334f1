using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainGameProgress :MonoBehaviour
{
    public static GameStaus gameStaus = GameStaus.GameStrat;
    static public MainGameProgress instance;
    [SerializeField] GameObject playerObj;


    public enum GameStaus
    {
        GameStrat,      //ゲーム開始
        ResetTurn,      //リセットターン
        PlayerTurn,     //プレイヤーターン
        Interval,       //インターバル
        ClearCheckNow,  //クリアチェック中
        GameClear,      //ゲームクリア
        GameOver,       //ゲームオーバー
    }

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameStaus)
        {
            case GameStaus.GameStrat:
            GameStartProgress();
            break;

            case GameStaus.ResetTurn:
            ResetTurnProgress();
            break;

            case GameStaus.PlayerTurn:
            PlayerTurnProgress();
            break;

            case GameStaus.Interval:
            IntervalProgress();
            break;

            case GameStaus.ClearCheckNow:
            ClearCheckNowProgress();
            break;
            
            case GameStaus.GameClear:
            GameClearProgress();
            break;

            case GameStaus.GameOver:
            GameOverProgress();
            break;

        }
    }

    void GameStartProgress()
    {
        Debug.Log("【進行】ゲームスタート");

        MainGameManager.nowTurn = 1;

        //変更予定
        //if (Input.GetMouseButtonDown(1))
        {
            gameStaus = GameStaus.ResetTurn;
        }
    }

    void ResetTurnProgress()
    {
        Debug.Log("【進行】リセットターン");
        Timer.CountReset();//タイマーリセット

        //アイテムidをどこから入れる？
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player").gameObject;
        }
        PassSystem.ItemPass(playerObj);//パス実行

        //変更予定
        gameStaus = GameStaus.PlayerTurn;
    }

    void PlayerTurnProgress()
    {
        Debug.Log("【進行】プレイヤーのターン");
        Timer.CountDown();

        //変更予定
        //if (Input.GetMouseButtonDown(1))//
        //{
        //    gameStaus = GameStaus.GameClear;
        //    SceneManager.SceneLaod(SceneManager.SceneName.ENDING);
        //}
        //if (Input.GetMouseButtonDown(2))
        //{
        //    gameStaus = GameStaus.GameOver;
        //}
    }

    void IntervalProgress()
    {
        Debug.Log("【進行】インターバル");
        SceneManager.SceneLaod(SceneManager.SceneName.INTERVAL);
    }

    void ClearCheckNowProgress()
    {
        Debug.Log("ゲームクリアチェック中");

        if(MainGameManager.nowTurn % 2 == 0) { //Bターン（偶数）の場合
            //先行と後攻のフラグによって遷移するシーンが変わる
            if(MainGameManager.isClearUserA && MainGameManager.isClearUserB)//AB脱出ルート
                gameStaus = GameStaus.GameClear;
            if(MainGameManager.isClearUserA || MainGameManager.isClearUserB) //AorB脱出ルート
                gameStaus = GameStaus.GameClear;
            else
                gameStaus = GameStaus.Interval;
        }
        else if(MainGameManager.nowTurn >= MainGameManager.maxTurn)//16ターン超えた場合
            gameStaus = GameStaus.GameOver;
        else
            gameStaus = GameStaus.Interval;

    }

    void GameClearProgress()
    {
        Debug.Log("【進行】ゲームクリア");

        SceneManager.SceneLaod(SceneManager.SceneName.ENDING);
        gameStaus = GameStaus.GameStrat;
    }

    void GameOverProgress()
    {
        Debug.Log("【進行】ゲームオーバ");

        SceneManager.SceneLaod(SceneManager.SceneName.ENDING);
        gameStaus = GameStaus.GameStrat;

    }
}
