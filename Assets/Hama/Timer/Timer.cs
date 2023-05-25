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
    float count;

    //カウント最大値
    [SerializeField] float countmax = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        count = countmax;
    }

    // Update is called once per frame
    void Update()
    {
        UIText.text = count.ToString("f0");
        CountDown();

        if(count < 0)
        {
            Debug.Log("インターバル画面に遷移");
        }
    }

    //カウントダウン
    void CountDown()
    {
        count -= Time.deltaTime;

        Debug.Log(count);
    }
}
