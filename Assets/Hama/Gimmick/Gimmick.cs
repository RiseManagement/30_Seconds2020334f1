using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    int stageitemName;

    //ギミック種類

    // Start is called before the first frame update
    void Start()
    {
        gimmmickFlag = false;
        stageitemName = int.Parse(gameObject.name);
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
            case 0://A絵画
                FiledObjChange();
                break;
            case 13:
                //謎1クリア
                break;
            case 14://台座
                FiledObjChange();
                break;
        }
        gimmmickFlag = false;
    }

    //使用後の事象発生

    /// <summary>
    /// フィールド物の変化
    /// </summary>
    public void FiledObjChange()
    {
        Debug.Log("フィールド上物の変化");
        //Debug.Log( gameObject.transform.GetComponent<SpriteRenderer>().sprite);
        gameObject.transform.GetComponent<SpriteRenderer>().sprite = ItemDataBase.Entity.GetData(stageitemName+1).Image;
    }

    public void NazoCler()
    {
        Debug.Log("謎クリア");
    }
}
