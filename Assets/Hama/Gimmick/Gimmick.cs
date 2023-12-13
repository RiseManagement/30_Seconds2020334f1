using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gimmick : MonoBehaviour
{
    //ギミック動作フラグ
   public bool gimmmickFlag;
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
        stageitemName = int.Parse(gameObject.name);
        gimmmickFlag = false;

        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            switch (stageitemName)
            {
                case 0://A絵画
                    FiledObjChange();
                    break;
                case 14://台座
                    FiledObjChange();
                    break;
                case 15:
                    //謎1クリア
                    MysteryCler();
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        stageitemName = int.Parse(gameObject.name);
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
            case 14://台座
                FiledObjChange();
                break;
            case 15:
                //謎1クリア
                MysteryCler();
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
        gameObject.transform.GetComponent<SpriteRenderer>().sprite = ItemDataBase.Entity.GetData(stageitemName+1).Image;
        this.gameObject.name = (stageitemName + 1).ToString();
    }

    public void MysteryCler()
    {   
        Debug.Log("謎クリア");
    }
}
