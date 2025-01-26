using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    //現在ターン数
    public static int nowTurn = 1;

    //最大ターン
    public static int maxTurn = 16;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
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
    public static void TurnCountUp()
    {
        if (nowTurn < maxTurn)
            nowTurn += 1;
    }
}
