using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gimmick : MonoBehaviour
{
    [Header("ギミック")]
    //ギミック動作フラグ
    public bool gimmickFlag;
    public bool GimmickFlag
    {
        get { return gimmickFlag; }
        set { gimmickFlag = value; }
    }

    //オブジェクト名
    int stageitemName;

    //ギミック種類

    //保持
    GameObject itemObj34;
    GameObject itemObj33;

    CameraManager cameraManager;

    //キャッシュした子オブジェクトの SpriteRenderer
    SpriteRenderer cachedSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクト名が整数でない場合は以降の処理をスキップ（毎フレームの int.Parse をやめる）
        if (!int.TryParse(gameObject.name, out stageitemName))
        {
            Debug.LogWarning("[Gimmick] gameObject.name を int に変換できません: " + gameObject.name);
            enabled = false;
            return;
        }

        var mainCameraObj = GameObject.Find("Main Camera");
        if (mainCameraObj != null)
        {
            cameraManager = mainCameraObj.GetComponent<CameraManager>();
        }

        // FieldObjChange 用に子オブジェクトの SpriteRenderer をキャッシュ
        if (transform.childCount > 0)
        {
            cachedSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        switch (stageitemName)
        {
            case 0://Ａ絵画
                // DeskOpen(case 2) と同じパターンに揃える。
                // default 分岐に流すと gimmickFlag=true となり、frame 1 で
                // GimmickAction.case0 が走って FieldObjChange が暴発し、
                // プレイヤー操作前に絵画が ID1(照射後) に切り替わってしまうので
                // 明示的にここで break してフラグを初期値(false)のままにする。
                break;
            case 2://袖机(中に絵具)
                //FieldObjChange();
                itemObj34 = GameObject.Find("34").gameObject;
                itemObj34.SetActive(false);
                // パズルクリア済み(シーン再入)なら机開放→絵具出現の連鎖を再適用する
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                    gimmickFlag = true;
                break;
            case 6://Ａ出口ドア
                gimmickFlag = false;
                break;
            case 8://Ａ鍵差込口
                gimmickFlag = false;
                break;
            case 10://ピアノ
                gimmickFlag = false;
                break;
            case 11://シリンダー
                gimmickFlag = false;
                break;
            case 14://台座
                gimmickFlag = false;
                break;
            case 15://台座(物乗っけてる)
                gimmickFlag = false;
                break;
            case 17://水槽(水無し)
                gimmickFlag = false;
                break;  
            case 24://Ｂ出口ドア
                gimmickFlag = false;
                break;
            case 26://Ｂ鍵差込口
                gimmickFlag = false;
                break;
            case 31://青ランプ(消灯)
                gimmickFlag = false;
                break;
            case 32://青ランプ(点灯)
                gimmickFlag = false;
                break;
            case 33://水抜きスイッチ(消灯)
                gimmickFlag = false;
                this.gameObject.SetActive(false);
                break;
            case 37://黄ランプ(消灯)
                gimmickFlag = false;
                break;
            case 38://黄ランプ(点灯)
                gimmickFlag = false;
                break;
            case 40://オルゴール
                gimmickFlag = false;
                break;
            case 42://黄ランプ(消灯)
                gimmickFlag = false;
                break;
            case 43://黄ランプ(点灯)
                gimmickFlag = false;
                break;
            case 47://水抜きスイッチ(点灯)
                gimmickFlag = false;
                break;
            default:
                gimmickFlag = true;
                break;
        }

        //画面以降後の反映状況の取得方法
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            GimmickAction();
        }

        if(ItemDataBase.Entity.GetData(stageitemName).EnabletakeFlag == 1)
        {
            if (ItemDataBase.Entity.GetData(stageitemName).OwnerFlag == 1 ||
                ItemDataBase.Entity.GetData(stageitemName).OwnerFlag == 2 ||
                ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
            {

                this.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ギミック内で gameObject.name を書き換えているため、差分があるときのみ再パース。
        // 毎フレーム int.Parse を呼ばないように変更前と比較する。
        if (!gameObject.name.Equals(stageitemName.ToString()))
        {
            if (int.TryParse(gameObject.name, out var newName))
            {
                stageitemName = newName;
            }
        }
        GimmickSelect();
    }

    /// <summary>
    /// 動作する各ギミック事象設定
    /// </summary>
    void GimmickSelect()
    {
        if (!gimmickFlag) return;

        GimmickAction();
    }

    /// <summary>
    /// ギミック動作
    /// </summary>
    void GimmickAction()
    {
        switch (stageitemName)
        {

            case 0://A絵画
                // ブラックライト(ID12)照射後にのみ 0→1(照射後) へ遷移する。
                // DropItem.case0 で ID0.InteractFlag=1 が立てられた次フレーム以降に発火。
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    FieldObjChange();
                    ObjChangeCheck();
                }
                break;
            case 2://袖机(中に絵具)
                DeskOpen();
                break;
            case 3:
                //本体活性
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    // 取得済み・使用済みの絵具(34)はシーン再入時に復活させない
                    if (itemObj34 != null &&
                        ItemDataBase.Entity.GetData(MysteryIds.Paint).OwnerFlag == 0 &&
                        ItemDataBase.Entity.GetData(MysteryIds.Paint).InteractFlag == 0)
                    {
                        itemObj34.SetActive(true);
                    }
                    ObjChangeCheck();
                }
                break;
            case 5://パズル
                PuzzleClear();
                MysteryClear();
                break;
            case 6://Ａ鍵差込口
                this.gameObject.GetComponentInChildren<Door>().animeStart();
                break;
            case 8://Ａ鍵差込口
                FieldObjChange();
                break;
            case 10:
                MysteryClear();
                break;
            case 14://台座
                FieldObjChange();
                break;
            case 15://謎1クリア
                MysteryClear();
                break;
            case 16://水槽(水あり)
                if (ItemInteractFlagCheck(33))//スイッチがONの場合
                {
                    FieldObjChange();
                    MysteryClear();
                }
                break;
            case 17://水槽(水無し)
                TankCylinderSet();
                // まず NAZO4A クリア判定 (stageitemName==17 かつ ClearCheck(17)==2 で発火)
                MysteryClear();
                // 仕様書 (ギミック発動詳細化一覧.xlsx 行14): シリンダー挿入後 17→18 に遷移
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    FieldObjChange(); // 17→18
                    ObjChangeCheck();
                }
                break;
            case 19://水槽の穴
                // 仕様書 (ギミック発動詳細化一覧.xlsx 行14): シリンダー挿入後に 19→20 に遷移
                // InteractFlag(19) が立つまでは遷移しない (シーン開始直後の即時遷移を防ぐ)
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    FieldObjChange();
                    ObjChangeCheck();
                }
                break;
            case 22://Ｂ絵画
                // 先に NAZO4B クリア判定 (stageitemName==22 かつ ClearCheck(22)==2 で発火)。
                // FieldObjChange(2) が先だと stageitemName が 35 になり case 22 に到達しない (case 17 と同じ順序に統一)
                MysteryClear();
                FieldObjChange(2); // 22→35
                ObjChangeCheck();
                break;
            case 24://Ｂ鍵差込口
                this.gameObject.GetComponentInChildren<Door>().animeStart();
                break;
            case 26://Ｂ鍵差込口
                FieldObjChange();
                break;
            case 28://花瓶
                // 仕様書 (ギミック発動詳細化一覧.xlsx 行15): 絵具(34)使用で 28→29 (花瓶(染色後)) に遷移
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    FieldObjChange(); // 28→29
                    ObjChangeCheck();
                }
                break;
            case 31://青ランプ(消灯)  
                FieldObjChange();
                ObjChangeCheck();
                break;
            case 32://青ランプ(点灯)
                FieldObjChange(1);
                ObjChangeCheck();
                break;
            case 33://水抜き水抜きスイッチ
                FieldObjChange(3);
                TankIsEmpty();
                break;
            case 36:
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    itemObj33 = GameObject.Find("33").gameObject;
                    itemObj33.SetActive(true);
                    ItemDataBase.Entity.GetData(stageitemName).ClearCheck = 2;

                }
                MysteryClear();
                break;
            case 37://黄ランプ(消灯)  
                FieldObjChange();
                ObjChangeCheck();
                break;
            case 38://黄ランプ(点灯)
                FieldObjChange(1);
                ObjChangeCheck();
                break;
            case 40://オルゴール
                // シリンダー使用 (DropItem.case40 で InteractFlag(40)=1) 後にのみ 40→41 へ遷移する。
                // 旧コードは FieldObjChange 後に 41 の InteractFlag を見ていたため gimmickFlag が永久に true のままで、
                // シリンダー取り出しで名前が 40 に戻った瞬間に再び 41 へ変わっていた。
                if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
                {
                    FieldObjChange(); // 40→41
                    MusicBoxMusicStart();
                }
                ObjChangeCheck();
                break;
            case 42://赤ランプ(消灯)  
                FieldObjChange();
                ObjChangeCheck();
                break;
            case 43://赤ランプ(点灯)
                FieldObjChange(1);
                ObjChangeCheck();
                break;
        }

    }

    /// <summary>
    /// パズルクリア
    /// </summary>
    void PuzzleClear()
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
            FieldObjChange(); // 2→3 (リネーム+スプライト変更)
            // ObjChangeCheck() は呼ばない:
            // gimmickFlag を維持し、次フレームの case 3 (絵具出現) まで連鎖させる。
            // フラグは case 3 側の ObjChangeCheck() で落ちる。
        }
    }

    /// <summary>
    /// オルゴール音再生
    /// </summary>
    void MusicBoxMusicStart()
    {
        // 呼び出し元 (case 40) で InteractFlag 判定済み。stageitemName は既に 41 になっている。
        Debug.Log("音楽流れた");
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

    /// <summary>
    /// 水槽（水無し）にシリンダーをセット
    /// </summary>
    void TankCylinderSet()
    {
        if (ItemDataBase.Entity.GetData(stageitemName).InteractFlag == 1)
        {
            var item = this.transform.Find("11").gameObject;
            item.SetActive(true);
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
    public void FieldObjChange(int updwon = 0)
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
                dataname = 47;//水抜きスイッチオフ→オン
                break;
            default:
                dataname = stageitemName + 1;
                break;
        }

        Debug.Log("フィールド上物の変化");
        // キャッシュ済みの SpriteRenderer を使用（フォールバックで取得も行う）
        if (cachedSpriteRenderer == null && transform.childCount > 0)
        {
            cachedSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        if (cachedSpriteRenderer != null)
        {
            cachedSpriteRenderer.sprite = ItemDataBase.Entity.GetData(dataname).Image;
        }
        this.gameObject.name = (dataname).ToString();
        stageitemName = dataname;
    }

    /// <summary>
    /// 謎クリア
    /// </summary>
    public void MysteryClear()
    {
        if (ItemDataBase.Entity.GetData(stageitemName).ClearCheck == 2)
        {
            Debug.Log("謎クリア");
            switch (stageitemName)
            {
                case 5:
                    MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO3A);
                    break;
                case 10:
                    MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO2);
                    break;
                case 17:
                    MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO4A);
                    break;
                case 22:
                    MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO4B);
                    break;
                case 36:
                    MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO3B);
                    break;
            }
        }

        // 仕様書準拠ディスパッチ (ギミック発動詳細化一覧.xlsx 行5):
        // 台座が 15 状態のとき、台座ボタン(ID13)経由で NAZO1 をクリアする。
        // シーン上の GameObject は "15" のままのため、stageitemName==15 の文脈で
        // ID13 のデータを参照して NAZO1 を立てる。
        if (stageitemName == 15 &&
            ItemDataBase.Entity.GetData(13).ClearCheck == 2)
        {
            Debug.Log("謎1クリア (ID13台座ボタン経由)");
            MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO1);
        }
    }

    public void ObjChangeCheck()
    {
        gimmickFlag = false;
    }
}
