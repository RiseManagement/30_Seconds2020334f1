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
        startTurnText.text = GameObject.Find("Player").GetComponent<User>().User_name + "のターン";
    }

    /// <summary>
    /// ターン開始時のプレイヤー名のフェードアウト
    /// </summary>
    void ChangeStartTurnTextColor()
    {
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

}
