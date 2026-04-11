using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイマークラス
/// </summary>

public class Timer :MonoBehaviour
{
    // UI Text指定用
    public Text UIText;

    //現在のカウント
    static float count;

    //カウント最大値
    [SerializeField] static float countmax = 30.0f;

    //カウントストップ用
    static public bool countstop;

    [SerializeField] Image timerImage;
    [SerializeField] GameObject timeupBgObj;

    // 直前にUIへ反映した整数秒（変化時のみ Text を更新してGCを減らす）
    int lastDisplayedCount = int.MinValue;
    // 直前のワーニング色適用状態
    bool warningColorApplied = false;
    static readonly Color WarningColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        count = countmax;
        countstop = false;
        lastDisplayedCount = int.MinValue;
        warningColorApplied = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 表示値が変わった時のみ Text を更新
        int displayed = Mathf.Max(0, Mathf.CeilToInt(count));
        if (displayed != lastDisplayedCount && UIText != null)
        {
            UIText.text = displayed.ToString();
            lastDisplayedCount = displayed;
        }

        //タイム残り5秒表示
        if ((count <= 5) && (count > 0))
        {
            if (!warningColorApplied && UIText != null)
            {
                UIText.color = WarningColor;
                warningColorApplied = true;
            }
            if (timeupBgObj != null && !timeupBgObj.activeSelf)
            {
                timeupBgObj.SetActive(true);
            }
        }
        else if (count < 0)
        {
            TurnEnd();
        }
    }
    public static void TurnEnd()
    {
        MainGameProgress.gameStatus = MainGameProgress.GameStatus.ClearCheckNow;
        CountReset();
    }

    //カウントダウン
    public static void CountDown()
    {
        if (countstop == true)
        {
            //Debug.Log("Timer停止");
        }
        else
        {
            count -= Time.deltaTime;
        }
        //Debug.Log(count);
    }

    public static void CountReset()
    {
        count = countmax;
    }

    /// <summary>
    /// インスタンス側のキャッシュもリセットする（色等）
    /// </summary>
    public void ResetDisplayState()
    {
        lastDisplayedCount = int.MinValue;
        warningColorApplied = false;
        if (UIText != null)
        {
            UIText.color = Color.white;
        }
        if (timeupBgObj != null)
        {
            timeupBgObj.SetActive(false);
        }
    }
}
