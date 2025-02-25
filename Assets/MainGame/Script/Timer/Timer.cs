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

    // Start is called before the first frame update
    void Start()
    {
        count = countmax;
        countstop = false;
    }

    // Update is called once per frame
    void Update()
    {
        UIText.text = count.ToString("f0");
        //タイム残り5秒表示
        if ((count <= 5) && (count > 0))
        {
            UIText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            timeupBgObj.SetActive(true);
        }
        else if (count < 0)
        {
            TurnEnd();
        }
    }
    public static void TurnEnd()
    {
        MainGameProgress.gameStaus = MainGameProgress.GameStaus.ClearCheckNow;
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
}
