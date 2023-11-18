using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    //ギミック動作フラグ
    bool gimmmickFlag;
    public bool GimmmickFlag
    {
        set
        {
            gimmmickFlag = value;
        }
    }

    //オブジェクト名
    string stageitemName;

    //ギミック種類

    // Start is called before the first frame update
    void Start()
    {
        gimmmickFlag = false;
        stageitemName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        GimmickSelect();
    }

    /// <summary>
    /// 動作する各ギミック事象設定
    /// </summary>
    void GimmickSelect()
    {
        if (!gimmmickFlag) return;

        switch (stageitemName)
        {
            case "0":
                AnswerItem();
                break;
        }
        gimmmickFlag = false;
    }

    //使用後の事象発生
    
    /// <summary>
    /// 正解アイテム表示
    /// </summary>
    public static void AnswerItem()
    {
        Debug.Log("正解アイテム表示");
    }

    /// <summary>
    /// フィールド物の変化
    /// </summary>
    public static void FiledObjChange()
    {
        Debug.Log("フィールド上物の変化");
    }
}
