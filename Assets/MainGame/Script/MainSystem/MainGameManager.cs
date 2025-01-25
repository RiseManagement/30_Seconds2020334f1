using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainGameManager :MonoBehaviour
{
    public static MainGameManager instance;
    public static bool isClearUserA;
    public static bool isClearUserB;
    
    //現在ターン数
    public static int nowTurn = 1;

    //最大ターン
    public static int maxTurn = 16;

    [SerializeField] Text startTurnText;
    float startTurnTextTimer = 0;
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
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeStartTurnTextColor();

        Debug.Log("ターン数:" + nowTurn);
    }
    void StartTurn()
    {
        startTurnTextTimer = 2;
        startTurnText.text = GameObject.Find("Player").GetComponent<User>().User_name + "のターン";
    }
    void ChangeStartTurnTextColor()
    {
        if(SceneTransitions.NowSceneName == "interval")

        if (startTurnTextTimer > 0)
        {
            startTurnTextTimer -= Time.deltaTime;
            if (startTurnTextTimer > 1)
            {
                startTurnText.color = new Color(startTurnText.color.r, startTurnText.color.g, startTurnText.color.b, 1);
            }
            else
            {
                startTurnText.color = new Color(startTurnText.color.r, startTurnText.color.g, startTurnText.color.b, startTurnTextTimer);
            }
        }
    }
    public void GoToEnding()
    {
        SceneTransitions.SceneLaod(SceneTransitions.SceneName.ENDING);
    }

    /// <summary>
    /// ターン数カウントアップ
    /// </summary>
    public static void TurnCountUp()
    {
        if(nowTurn < maxTurn)
            nowTurn += 1;
    }
}
