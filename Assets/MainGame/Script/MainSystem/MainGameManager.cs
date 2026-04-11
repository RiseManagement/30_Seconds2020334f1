using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainGameManager :MonoBehaviour
{
    public static bool isClearUserA;
    public static bool isClearUserB;

    [SerializeField] Text startTurnText;
    float startTurnTextTimer = 0;

    // 直前に適用したアルファ値（変化時のみ色を更新してダーティフラグを抑制）
    float lastAppliedAlpha = -1f;

    // Start is called before the first frame update
    void Start()
    {
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeStartTurnTextColor();

        //Debug.Log("ターン数:" + nowTurn);
    }
    void StartTurn()
    {
        startTurnTextTimer = 2;
        lastAppliedAlpha = -1f;

        if (startTurnText == null)
        {
            Debug.LogWarning("[MainGameManager] startTurnText が未設定です。");
            return;
        }

        var playerObj = GameObject.Find("Player");
        if (playerObj == null)
        {
            Debug.LogWarning("[MainGameManager] Playerオブジェクトが見つかりません。");
            startTurnText.text = "のターン";
            return;
        }

        var user = playerObj.GetComponent<User>();
        if (user == null)
        {
            Debug.LogWarning("[MainGameManager] PlayerにUserコンポーネントがありません。");
            startTurnText.text = "のターン";
            return;
        }
        startTurnText.text = user.User_name + "のターン";
    }

    /// <summary>
    /// ターン開始時のプレイヤー名のフェードアウト
    /// </summary>
    void ChangeStartTurnTextColor()
    {
        if (startTurnText == null) return;
        if (startTurnTextTimer <= 0) return;

        startTurnTextTimer -= Time.deltaTime;

        float targetAlpha = (startTurnTextTimer > 1) ? 1f : Mathf.Max(0f, startTurnTextTimer);

        // アルファ値に変化がある時だけ Color 代入（UIの再描画抑制）
        if (!Mathf.Approximately(lastAppliedAlpha, targetAlpha))
        {
            var c = startTurnText.color;
            c.a = targetAlpha;
            startTurnText.color = c;
            lastAppliedAlpha = targetAlpha;
        }
    }

}
