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
        IntervalStart,  //インターバル開始
        IntervalEnd,    //インターバル終了
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
        //Debug.Log("プロセス：" + gameStaus);

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

            case GameStaus.IntervalStart:
            IntervalStartProgress();
            break;

            case GameStaus.IntervalEnd:
            IntervalEndProgress();
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

        TurnManager.nowTurn = 1;

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
        //Debug.Log("【進行】プレイヤーのターン");
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

    void IntervalStartProgress()
    {
        Debug.Log("【進行】インターバル開始");
        SceneTransitions.SceneLaod(SceneTransitions.SceneName.INTERVAL);
        gameStaus = GameStaus.IntervalEnd;
    }
    void IntervalEndProgress()
    {
        Debug.Log("【進行】インターバル終了");
    }

    void ClearCheckNowProgress()
    {
        Debug.Log("ゲームクリアチェック中");

        if(TurnManager.nowTurn % 2 == 0) { //Bターン（偶数）の場合
            //先行と後攻のフラグによって遷移するシーンが変わる
            if(MainGameManager.isClearUserA && MainGameManager.isClearUserB)//AB脱出ルート
                gameStaus = GameStaus.GameClear;
            if(MainGameManager.isClearUserA || MainGameManager.isClearUserB) //AorB脱出ルート
                gameStaus = GameStaus.GameClear;
            else
                gameStaus = GameStaus.IntervalStart;
        }
        else if(TurnManager.nowTurn >= TurnManager.maxTurn)//16ターン超えた場合
            gameStaus = GameStaus.GameOver;
        else
            gameStaus = GameStaus.IntervalStart;

    }

    void GameClearProgress()
    {
        Debug.Log("【進行】ゲームクリア");

        SceneTransitions.SceneLaod(SceneTransitions.SceneName.ENDING);
        gameStaus = GameStaus.GameStrat;
    }

    void GameOverProgress()
    {
        Debug.Log("【進行】ゲームオーバ");

        SceneTransitions.SceneLaod(SceneTransitions.SceneName.ENDING);
        gameStaus = GameStaus.GameStrat;

    }
}
