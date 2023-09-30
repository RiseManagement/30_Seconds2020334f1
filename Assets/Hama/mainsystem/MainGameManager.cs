using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainGameManager :MonoBehaviour
{
    public static MainGameManager instance;
    //現在ターン数
    public static int nowTurn = 1;

    //最大ターン数
    int maxTurn = 16;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ターン数カウントアップ
    /// </summary>
    public void TurnCountUp()
    {
        nowTurn += 1;
    }
}
