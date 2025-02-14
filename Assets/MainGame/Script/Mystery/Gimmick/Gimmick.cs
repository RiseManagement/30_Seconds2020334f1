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
    GameObject itemObj33;

    // Start is called before the first frame update
    void Start()
    {
        stageitemName = int.Parse(gameObject.name);

        switch (stageitemName)
        {
            case 2://袖机(中に絵具)
                //FiledObjChange();
                itemObj34 = GameObject.Find("34").gameObject;
                itemObj34.SetActive(false);
                break;
            case 6://Ａ出口ドア
                gimmmickFlag = false;
                break;
            case 8://Ａ鍵差込口
                gimmmickFlag = false;
                break;
            case 24://Ｂ出口ドア
                gimmmickFlag = false;
                break;
            case 26://Ｂ鍵差込口
                gimmmickFlag = false;
                break;
            case 31://青ランプ(消灯)
                gimmmickFlag = false;
                break;
            case 32://青ランプ(点灯)
                gimmmickFlag = false;
                break;
            case 33://水抜きスイッチ(消灯)
                gimmmickFlag = false;
                this.gameObject.SetActive(false);
                break;
            case 37://黄ランプ(消灯)
                gimmmickFlag = false;
                break;
            case 38://黄ランプ(点灯)
                gimmmickFlag = false;
                break;
            case 40://オルゴール
                gimmmickFlag = false;
                break;
            case 42://黄ランプ(消灯)
                gimmmickFlag = false;
                break;
            case 43://黄ランプ(点灯)
                gimmmickFlag = false;
                break;
            case 47://水抜きスイッチ(点灯)
                gimmmickFlag = false;
                break;
            default:
                gimmmickFlag = true;
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
                case 8://Ａ鍵差込口

                    break;
                case 14://台座
                    FiledObjChange();
                    break;
                case 15:
                    //謎1クリア
                    MysteryCler();
                    break;
                case 16://水槽(水有り)
                    if(ItemDataBase.Entity.GetData(33).InteractFlag == 1)
                    {
                        FiledObjChange();
                    }
                    break;
                case 17://水槽(水無し)
                    //FiledObjChange();
                    break;
                case 19://水槽の穴
                    FiledObjChange();
                    break;
                case 22://Ｂ絵画
                    FiledObjChange(2);
                    break;
                case 28://花瓶
                    FiledObjChange();
                    MusicBoxMusicStart();
                    break;
                case 33://水抜きスイッチ(消灯)
                    FiledObjChange(3);
                    this.gameObject.SetActive(true);
                    break;
                case 36:
                    if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 0)
                    {
                        itemObj33 = GameObject.Find("33").gameObject;
                        itemObj33.SetActive(false);
                    }
                    break;
                case 40://オルゴール
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
                MysteryCler();
                break;
            case 6://Ａ鍵差込口
                this.gameObject.GetComponentInChildren<Door>().animeStart();
                break;
            case 8://Ａ鍵差込口
                FiledObjChange();
                break;
            case 10:
                MysteryCler();
                break;
            case 14://台座
                FiledObjChange();
                break;
            case 15://謎1クリア
                MysteryCler();
                break;
            case 16://水槽(水あり)
                if (ItemInteractFlagCheck(33))//スイッチがONの場合
                {
                    FiledObjChange();
                    MysteryCler();
                }
                break;
            case 17://水槽(水無し)
                //FiledObjChange();
                //MysteryCler();
                break;
            case 19://水槽の穴
                FiledObjChange();
                break;
            case 22://Ｂ絵画
                FiledObjChange(2);
                MysteryCler();
                break;
            case 24://Ｂ鍵差込口
                this.gameObject.GetComponentInChildren<Door>().animeStart();
                break;
            case 26://Ｂ鍵差込口
                FiledObjChange();
                break;
            case 31://青ランプ(消灯)  
                FiledObjChange();
                ObjChangeCheck();
                break;
            case 32://青ランプ(点灯)
                FiledObjChange(1);
                ObjChangeCheck();
                break;
            case 33://水抜き水抜きスイッチ
                FiledObjChange(3);
                TankIsEmpty();
                break;
            case 36:
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    itemObj33 = GameObject.Find("33").gameObject;
                    itemObj33.SetActive(true);
                }
                MysteryCler();
                break;
            case 37://黄ランプ(消灯)  
                FiledObjChange();
                ObjChangeCheck();
                break;
            case 38://黄ランプ(点灯)
                FiledObjChange(1);
                ObjChangeCheck();
                break;
            case 40://オルゴール
                FiledObjChange();
                MusicBoxMusicStart();
                break;
            case 42://赤ランプ(消灯)  
                FiledObjChange();
                ObjChangeCheck();
                break;
            case 43://赤ランプ(点灯)
                FiledObjChange(1);
                ObjChangeCheck();
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

    /// <summary>
    /// 机の引き出し開放
    /// </summary>
    void DeskOpen()
    {
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            Debug.Log("デスクオープン");
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

    /// <summary>
    /// 水槽の水がなくなる
    /// </summary>
    void TankIsEmpty()
    {
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            Debug.Log("水槽の水がなくなった");
            ObjChangeCheck();
        }
    }

    bool ItemInteractFlagCheck(int no)
    {
        bool flag = false;

        if (ItemDataBase.Entity.GetData(no).InteractFlag == 1)
            flag = true;

        return flag;
    }

    //使用後の事象発生

    /// <summary>
    /// フィールド物の変化
    /// </summary>
    public void FiledObjChange(int updwon = 0)
    {
        int dataname;
        switch (updwon)
        {
            case 0:
                dataname = stageitemName + 1;
                break;
            case 1:
                dataname = stageitemName - 1;
                break;
            case 2:
                dataname = 35;//Ｂ絵画→B絵画(使用後)
                break;
            case 3:
                dataname = 46;//水抜きスイッチオフ→オン
                break;
            default:
                dataname = stageitemName + 1;
                break;
        }

        Debug.Log("フィールド上物の変化");
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ItemDataBase.Entity.GetData(dataname).Image;
        this.gameObject.name = (dataname).ToString();
    }

    /// <summary>
    /// 謎クリア
    /// </summary>
    public void MysteryCler()
    {
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            Debug.Log("謎クリア");
            switch (stageitemName)
            {
                case 5:
                    MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO3A);
                    break;
                case 10:
                    MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO2);
                    break;
                case 15:
                    MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO1);
                    break;
                case 17:
                    MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO4A);
                    break;
                case 22:
                    MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO4B);
                    break;
                case 36:
                    MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO3B);
                    break;

            }
        }
    }

    public void ObjChangeCheck()
    {
        gimmmickFlag = false;
    }
}
