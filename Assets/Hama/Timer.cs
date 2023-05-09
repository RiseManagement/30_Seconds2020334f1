using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイマークラス
/// </summary>

public class Timer : MonoBehaviour
{
    //現在のカウント
    float count;

    //カウント最大値
    [SerializeField] float countmax = 30;

    // Start is called before the first frame update
    void Start()
    {
        count = countmax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //カウントダウン
    void CountDown()
    {
        count = count - Time.deltaTime;
    }
}
