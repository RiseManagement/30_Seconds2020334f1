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
            case 5://パズル
                PazzleClear();
                //パズルクリア後机から絵具入手可能（パズルクリア→２→３）
                
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
        gimmmickFlag = false;
    }

    /// <summary>
    /// パズルクリア
    /// </summary>
    void PazzleClear()
    {
        if(ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            ItemDataBase.Entity.GetData(2).InteractFlag = 1;
        }
    }

    void DeskOpen()
    {
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            FiledObjChange();
        }
    }

    /// <summary>
    /// オルゴール音再生
    /// </summary>
    void MusicBoxMusicStart()
    {
        Debug.Log("音楽流れた");
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

    public void ObjChangeCheck()
    {
        gimmmickFlag = false;
    }
}
