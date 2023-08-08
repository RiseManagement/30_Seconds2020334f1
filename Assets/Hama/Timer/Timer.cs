using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイマークラス
/// </summary>

public class Timer : MonoBehaviour
{
    // UI Text指定用
    public Text UIText;

    //現在のカウント
    static float count;

    //カウント最大値
    [SerializeField] static float countmax = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        count = countmax;
    }

    // Update is called once per frame
    void Update()
    {
        UIText.text = count.ToString("f0");
        if ((count<=5)&&(count>0))
        {
            UIText.color = new Color(1.0f,0.0f,0.0f,1.0f);
            Debug.Log("Timer_Red");
        }
        else if(count < 0)
        {
            MainGameProgress.gameStaus = MainGameProgress.GameStaus.Interval;
            SceneManager.SceneLaod(SceneManager.SceneName.INTERVAL);
        }
    }

    //カウントダウン
    public static void CountDown()
    {
        count -= Time.deltaTime;

        //Debug.Log(count);
    }

    public static void CountReset()
    {
        count = countmax;
    }
}
