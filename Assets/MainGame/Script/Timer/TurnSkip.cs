using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnSkip : MonoBehaviour, IPointerClickHandler
{
    //ダブルクリックの判定時間
    public float doubleClickTime = 0.3f;

    //最後にクリックした時間
    private float lastClickTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// アイテムをタップ時の処理
    /// </summary>
    /// <param name="eventData">タップ情報</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 左クリックの場合  
        {
            if (Time.time - lastClickTime < doubleClickTime)
            {
                OnDoubleClick();
            }
            lastClickTime = Time.time;
        }
    }

    private void OnDoubleClick()
    {
        //Debug.Log("ダブルクリックが検出されました！");
        // ダブルクリック時の処理をここに追加

        Timer.TurnEnd();
    }
}
