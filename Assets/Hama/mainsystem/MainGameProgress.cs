using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameProgress : MonoBehaviour
{
    public static GameStaus gameStaus = GameStaus.GameStrat;
    static public MainGameProgress instance;
    [SerializeField] GameObject playerObj;


    public enum GameStaus
    {
        GameStrat,  //ゲーム開始
        Wait,       //待機
        PlayerTurn, //プレイヤーターン
        Interval,   //インターバル
        GameClear,  //ゲームクリア
        GameOver,   //ゲームオーバー
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

            case GameStaus.Wait:
                WaitProgress();
                break;

            case GameStaus.PlayerTurn:
                PlayerTurnProgress();
                break;

            case GameStaus.Interval:
                IntervalProgress();
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

        //変更予定
        if (Input.GetMouseButtonDown(1))
        {
            gameStaus = GameStaus.Wait;
        }
    }

    void WaitProgress()
    {
        Debug.Log("【進行】待ち");
        Timer.CountReset();

        //アイテムidをどこから入れる？
        PassSystem.ItemPass(playerObj);

        //変更予定
        if (Input.GetMouseButtonDown(1))
        {
            gameStaus = GameStaus.PlayerTurn;
        }
    }

    void PlayerTurnProgress()
    {
        Debug.Log("【進行】プレイヤーのターン");
        Timer.CountDown();

        //変更予定
        if (Input.GetMouseButtonDown(1))
        {
            gameStaus = GameStaus.GameClear;
            SceneManager.SceneLaod(SceneManager.SceneName.CLEAR);
        }
        if (Input.GetMouseButtonDown(2))
        {
            gameStaus = GameStaus.GameOver;
        }
    }

    void IntervalProgress()
    {
        Debug.Log("【進行】インターバル");
    }

    void GameClearProgress()
    {
        Debug.Log("【進行】ゲームクリア");

        //変更予定
        if (Input.GetMouseButtonDown(0))
        {
            gameStaus = GameStaus.GameStrat;
            SceneManager.SceneLaod(SceneManager.SceneName.MAINGAME_A);
        }
    }

    void GameOverProgress()
    {
        Debug.Log("【進行】ゲームオーバ");

        //変更予定
        if (Input.GetMouseButtonDown(0))
        {
            gameStaus = GameStaus.GameStrat;
        }
    }
}
