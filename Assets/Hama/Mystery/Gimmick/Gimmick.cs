using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Gimmick : MonoBehaviour
{
    [Header("ギミック")]
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

    //保持
    GameObject itemObj34;

    // Start is called before the first frame update
    void Start()
    {
        stageitemName = int.Parse(gameObject.name);
        gimmmickFlag = true;

        switch (stageitemName)
        {
            case 2://袖机(中に絵具)
                   //FiledObjChange();
                itemObj34 = GameObject.Find("34").gameObject;
                itemObj34.SetActive(false);
                break;
        }

        //画面以降後の反映状況の取得方法
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            switch (stageitemName)
            {
                case 0://A絵画
                    FiledObjChange();
                    break;
                case 2://袖机(中に絵具)
                    FiledObjChange();
                    break;
                case 14://台座
                    FiledObjChange();
                    break;
                case 15:
                    //謎1クリア
                    MysteryCler();
                    break;
                case 40:
                    //オルゴール
                    FiledObjChange();
                    MusicBoxMusicStart();
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
            case 2://袖机(中に絵具)
                DeskOpen();
                break;
            case 3:
                //本体活性
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    itemObj34.SetActive(true);
                    ObjChangeCheck();
                }
                break;
            case 5://パズル
                PazzleClear();
                break;
            case 14://台座
                FiledObjChange();
                break;
            case 15:
                //謎1クリア
                MysteryCler();
                break;
            case 40:
                //オルゴール
                FiledObjChange();
                MusicBoxMusicStart();
                break;
        }
        
    }

    /// <summary>
    /// パズルクリア
    /// </summary>
    void PazzleClear()
    {
        //アイテム5が使用済
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            Debug.Log("パズルクリア後ギミック");
            ObjChangeCheck();
        }
    }

    void DeskOpen()
    {
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            FiledObjChange();
            ObjChangeCheck();
        }
    }

    /// <summary>
    /// オルゴール音再生
    /// </summary>
    void MusicBoxMusicStart()
    {
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            Debug.Log("音楽流れた");
            ObjChangeCheck();
        }
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
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            Debug.Log("謎クリア");
        }
    }

    public void ObjChangeCheck()
    {
        gimmmickFlag = false;
    }
}
