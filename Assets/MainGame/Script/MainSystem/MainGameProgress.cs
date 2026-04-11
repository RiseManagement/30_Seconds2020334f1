using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainGameProgress :MonoBehaviour
{
    public static GameStatus gameStatus = GameStatus.GameStart;
    static public MainGameProgress instance;
    [SerializeField] GameObject playerObj;


    public enum GameStatus
    {
        GameStart,      //ゲーム開始
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
        //Debug.Log("プロセス：" + gameStatus);

        switch (gameStatus)
        {
            case GameStatus.GameStart:
            GameStartProgress();
            break;

            case GameStatus.ResetTurn:
            ResetTurnProgress();
            break;

            case GameStatus.PlayerTurn:
            PlayerTurnProgress();
            break;

            case GameStatus.IntervalStart:
            IntervalStartProgress();
            break;

            case GameStatus.IntervalEnd:
            IntervalEndProgress();
            break;

            case GameStatus.ClearCheckNow:
            ClearCheckNowProgress();
            break;
            
            case GameStatus.GameClear:
            GameClearProgress();
            break;

            case GameStatus.GameOver:
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
            gameStatus = GameStatus.ResetTurn;
        }
    }

    void ResetTurnProgress()
    {
        Debug.Log("【進行】リセットターン");
        Timer.CountReset();//タイマーリセット

        //アイテムidをどこから入れる？
        if (playerObj == null)
        {
            var foundPlayer = GameObject.Find("Player");
            if (foundPlayer == null)
            {
                Debug.LogWarning("[MainGameProgress] Playerオブジェクトが見つかりません。パス処理をスキップします。");
                gameStatus = GameStatus.PlayerTurn;
                return;
            }
            playerObj = foundPlayer;
        }
        PassSystem.ItemPass(playerObj);//パス実行

        //変更予定
        gameStatus = GameStatus.PlayerTurn;
    }

    void PlayerTurnProgress()
    {
        //Debug.Log("【進行】プレイヤーのターン");
        Timer.CountDown();

        //変更予定
        //if (Input.GetMouseButtonDown(1))//
        //{
        //    gameStatus = GameStatus.GameClear;
        //    SceneManager.SceneLaod(SceneManager.SceneName.ENDING);
        //}
        //if (Input.GetMouseButtonDown(2))
        //{
        //    gameStatus = GameStatus.GameOver;
        //}
    }

    void IntervalStartProgress()
    {
        Debug.Log("【進行】インターバル開始");
        SceneTransitions.SceneLaod(SceneTransitions.SceneName.INTERVAL);
        gameStatus = GameStatus.IntervalEnd;
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
            // AB両方脱出ルート／AorB脱出ルートで条件が重複しないように else if で判定する
            if(MainGameManager.isClearUserA && MainGameManager.isClearUserB)//AB脱出ルート
            {
                gameStatus = GameStatus.GameClear;
            }
            else if(MainGameManager.isClearUserA || MainGameManager.isClearUserB) //AorB脱出ルート
            {
                gameStatus = GameStatus.GameClear;
            }
            else if(TurnManager.nowTurn >= TurnManager.maxTurn)//16ターン超えた場合
            {
                gameStatus = GameStatus.GameOver;
            }
            else
            {
                gameStatus = GameStatus.IntervalStart;
            }
        }
        else if(TurnManager.nowTurn >= TurnManager.maxTurn)//16ターン超えた場合
        {
            gameStatus = GameStatus.GameOver;
        }
        else
        {
            gameStatus = GameStatus.IntervalStart;
        }

    }

    void GameClearProgress()
    {
        Debug.Log("【進行】ゲームクリア");

        SceneTransitions.SceneLaod(SceneTransitions.SceneName.ENDING);
        gameStatus = GameStatus.GameStart;
    }

    void GameOverProgress()
    {
        Debug.Log("【進行】ゲームオーバ");

        SceneTransitions.SceneLaod(SceneTransitions.SceneName.ENDING);
        gameStatus = GameStatus.GameStart;

    }
}
