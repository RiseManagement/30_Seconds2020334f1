using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 周回プレイ対応: タイトルシーン読込時にゲーム進行系の static 変数を一括初期化する。
/// (ItemDataBase のランタイムデータは ItemDataBase 側の同種フックで初期化される)
/// 新しい static 進行フラグを追加した場合は ResetAll() にも追記すること。
/// </summary>
public static class GameStateReset
{
    static bool registered;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Register()
    {
        // Enter Play Mode Options (ドメインリロード無効) でも初回から確実に初期化し、二重購読を防ぐ
        ResetAll();
        if (registered) return;
        registered = true;

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name.Equals("title", System.StringComparison.OrdinalIgnoreCase))
            {
                ResetAll();
            }
        };
    }

    /// <summary>
    /// ゲーム進行に関わる static 変数をすべて初期値に戻す。
    /// </summary>
    public static void ResetAll()
    {
        // クリア状態
        MainGameManager.isClearUserA = false;
        MainGameManager.isClearUserB = false;
        MysteryManager.ResetAll();

        // 進行状態
        MainGameProgress.gameStatus = MainGameProgress.GameStatus.GameStart;
        TurnManager.nowTurn = 1;
        PassSystem.passitemid = -1;

        // タイマー
        Timer.countstop = false;
        Timer.CountReset();

        // 前周回の DontDestroyOnLoad プレイヤーを破棄
        if (User.playerObj != null)
        {
            Object.Destroy(User.playerObj);
            User.playerObj = null;
        }

        // プレイヤー名
        NameInputcontroller.player_1name = "";
        NameInputcontroller.player_2name = "";

        Debug.Log("[GameStateReset] ゲーム進行のstatic変数を初期化しました");
    }
}
