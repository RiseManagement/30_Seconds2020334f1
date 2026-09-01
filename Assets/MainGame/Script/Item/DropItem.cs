using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropItem : MonoBehaviour, IPointerClickHandler
{
    ItemSlot itemslot;

    new CameraManager camera;
    public GameObject stageitemobj;
    public int stageitemNumber;

    //public int old_stageitemNumber;

    private void Start()
    {
        var itemSlotObj = GameObject.Find("ItemSlot");
        if (itemSlotObj != null)
        {
            itemslot = itemSlotObj.GetComponent<ItemSlot>();
        }
        if (itemslot == null)
        {
            Debug.LogWarning("[DropItem] ItemSlot が見つかりません。");
        }

        var mainCameraObj = GameObject.Find("Main Camera");
        if (mainCameraObj != null)
        {
            camera = mainCameraObj.GetComponent<CameraManager>();
        }
        if (camera == null)
        {
            Debug.LogWarning("[DropItem] Main Camera の CameraManager が見つかりません。");
        }

        HideIfAlreadyOwned();
    }

    private void OnEnable()
    {
        HideIfAlreadyOwned();
    }

    private void HideIfAlreadyOwned()
    {
        if (!int.TryParse(gameObject.name, out var id))
        {
            return;
        }

        var data = ItemDataBase.Entity.GetData(id);
        if (data.EnabletakeFlag == 1 &&
            (data.OwnerFlag == 1 || data.OwnerFlag == 2 || data.InteractFlag == 1))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// アイテムをタップ時の処理
    /// </summary>
    /// <param name="eventData">タップ情報</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"オブジェクト {name} がクリックされたよ！");
        GetStageItemTapObjectInfo();
        if (stageitemobj == null)
        {
            return;
        }

        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);

        //所持可能なアイテムをタップした場合
        if (ItemDataBase.Entity.GetData(stageitemNumber).EnabletakeFlag == 1)
        {
            //所持する前の操作
            int tappedItemNumber = stageitemNumber; // 変化前のID退避 (case 41でstageitemNumberが11に書き換わるため)
            switch (stageitemNumber)
            {
                case 21://A宝箱(A鍵)
                    if (!MysteryManager.MysteryAllClearCheck()) return;
                    break;
                case 30://B宝箱（B鍵）
                    if (!MysteryManager.MysteryAllClearCheck()) return;
                    break;
                case 41://オルゴール(シリンダーあり)
                    stageitemNumber = 11;
                    break;
                default:
                    break;
            }

            //イベントり追加
            //アイテムの詳細画面の表示非表示をObject名を参照して行う
            //ここにitem_Detailの関数を書く
            var itemDetailObj = GameObject.Find("ItemDetailController");
            if (itemDetailObj != null)
            {
                var itemDetail = itemDetailObj.GetComponent<Item_Detail>();
                if (itemDetail != null)
                {
                    itemDetail.ItemDropDetail(stageitemNumber);
                }
            }

            //イベントリ追加
            if (Inventory.instance != null)
            {
                Inventory.instance.Add(stageitemNumber);
            }

            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                //所有者設定
                if (playerObj.GetComponent<User_A>())
                {
                    //Debug.Log("Aが取得");
                    ItemDataBase.Entity.GetData(stageitemNumber).OwnerFlag = 1;
                }
                else if (playerObj.GetComponent<User_B>())
                {
                    //Debug.Log("Bが取得");
                    ItemDataBase.Entity.GetData(stageitemNumber).OwnerFlag = 2;
                }
            }
            else
            {
                Debug.LogWarning("[DropItem] Playerオブジェクトが見つかりません。所有者設定をスキップ。");
            }

            //ステージ上のアイテム変化
            // stageitemNumberはcase 41で11に書き換わるため、タップ時のIDで分岐する (旧コードはcase 41がデッドコードでオルゴールが消滅していた)
            switch (tappedItemNumber)
            {
                case 21://A宝箱(A鍵)
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ItemDataBase.Entity.GetData(45).Image;
                    this.gameObject.name = (45).ToString();
                    ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    break;
                case 30://B宝箱（B鍵）
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ItemDataBase.Entity.GetData(45).Image;
                    this.gameObject.name = (45).ToString();
                    ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    break;
                case 41://オルゴール(シリンダーあり)
                    gameObject.transform.GetComponent<SpriteRenderer>().sprite = ItemDataBase.Entity.GetData(40).Image;
                    this.gameObject.name = (40).ToString();
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            if (camera == null)
            {
                Debug.LogWarning("[DropItem] CameraManager 参照が null のため処理を中断します。");
                return;
            }

            //フォーカス対象＋カメラフォーカスではない
            if (ItemDataBase.Entity.GetData(stageitemNumber).FocusPower > 0 && !camera.Focusflg)
            {
                Debug.Log("フォーカスではない");
                //フォーカス
                Debug.Log("フォーカス対象アイテム：" + stageitemobj.name);
                camera.ItemFocus(new Vector2(stageitemobj.transform.position.x, stageitemobj.transform.position.y), ItemDataBase.Entity.GetData(stageitemNumber).FocusPower, stageitemobj.transform);
            }
            else
            {
                Debug.Log("フォーカス中");
                switch (stageitemNumber)
                {
                    case 0://アイテム選択された側
                        if (itemslot.itemid == 12)//アイテム使用側
                        {
                            StageItemGimmickOn();
                            Inventory.instance.Removed(itemslot.itemid);
                            itemslot.ItemUse();

                            //アイテム選択側は使用済み更新
                            ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        }
                        break;
                    case 4://パズル

                        break;
                    case 6://Ａ出口ドア
                        if (ItemDataBase.Entity.GetData(8).InteractFlag == 1)//鍵を開けてる場合
                        {
                            StageItemGimmickOn();
                            itemslot.ItemUse();
                        }
                        break;
                    case 8://Ａ鍵差込口
                        if (itemslot.itemid == 21)//アイテム使用側
                        {
                            StageItemGimmickOn();
                            Inventory.instance.Removed(itemslot.itemid);
                            itemslot.ItemUse();

                            //アイテム選択側は使用済み更新
                            ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        }
                        break;
                    case 10://アップライトピアノ
                        if(ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag == 0)
                        {
                            if(stageitemobj.transform.GetChild(0).gameObject != null)
                                stageitemobj.transform.GetChild(0).gameObject.SetActive(true);
                            Debug.Log("ピアノタップ");
                        }
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;

                        break;
                    case 14://台座
                        if (itemslot.itemid == 39)//リンゴ
                        {
                            //事象処理
                            StageItemGimmickOn();
                            Inventory.instance.Removed(itemslot.itemid);
                            itemslot.ItemUse();
                            ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        }
                        break;
                    case 15://台座(物乗っけてる) → 台座ボタン(ID13) 押下と見做す
                        StageItemGimmickOn();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        // 仕様書(ギミック発動詳細化一覧.xlsx 行5):
                        // 「アイテムID15の状態でアイテムID13(台座ボタン)に触れる。謎1クリア。」
                        // シーン上の GameObject は "15" のまま（FieldObjChange で名前が 14→15 に変わる）だが、
                        // データ層では ID13 の InteractFlag/ClearCheck を立て、Gimmick.MysteryClear() の
                        // ID13 経由ディスパッチで NAZO1 をクリアする。
                        ItemDataBase.Entity.GetData(13).InteractFlag = 1;
                        ItemDataBase.Entity.GetData(13).ClearCheck = 2;
                        break;
                    case 17://水槽空
                        if (itemslot.itemid == 11)//シリンダー
                        {
                            //事象処理
                            StageItemGimmickOn();
                            int useditemid = itemslot.itemid;//ItemUse()でitemidが-1になる前に退避
                            Inventory.instance.Removed(useditemid);
                            itemslot.ItemUse();
                            ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                            ItemDataBase.Entity.GetData(useditemid).OwnerFlag = 0;
                            ItemDataBase.Entity.GetData(useditemid).ClearCheck = 2;
                            // 仕様書 (ギミック発動詳細化一覧.xlsx 行14):
                            // 「アイテムID19→20（水槽の穴）。アイテムID17→18（水槽）。謎4Aクリア。」
                            // 水槽の穴(19) にも InteractFlag を立てて Gimmick.case 19 経由で 19→20 に遷移させ、
                            // 水槽(17) の ClearCheck を 2 にして Gimmick.MysteryClear() 経由で NAZO4A をクリアする。
                            ItemDataBase.Entity.GetData(19).InteractFlag = 1;
                            ItemDataBase.Entity.GetData(stageitemNumber).ClearCheck = 2;
                        }
                        break;
                    case 22://Ｂ絵画
                        if (itemslot.itemid == 29)//花瓶(染色後)
                        {
                            //事象処理
                            StageItemGimmickOn();
                            Inventory.instance.Removed(itemslot.itemid);
                            itemslot.ItemUse();
                            ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                            ItemDataBase.Entity.GetData(stageitemNumber).ClearCheck = 2;
                        }
                        break;
                    case 24://Ｂ出口ドア
                        if (ItemDataBase.Entity.GetData(26).InteractFlag == 1)//鍵を開けてる場合
                        {
                            StageItemGimmickOn();
                            itemslot.ItemUse();
                        }
                        break;
                    case 26://Ｂ鍵差込口
                        if (itemslot.itemid == 30)//アイテム使用側
                        {
                            StageItemGimmickOn();
                            Inventory.instance.Removed(itemslot.itemid);
                            itemslot.ItemUse();

                            //アイテム選択側は使用済み更新
                            ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        }
                        break;
                    case 28://花瓶
                        if (itemslot.itemid == 34)//絵具
                        {
                            //事象処理
                            StageItemGimmickOn();
                            Inventory.instance.Removed(itemslot.itemid);
                            itemslot.ItemUse();
                            ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                            ItemDataBase.Entity.GetData(stageitemNumber + 1).InteractFlag = 1;
                            // 仕様書 (ギミック発動詳細化一覧.xlsx 行15/16):
                            // 「花瓶（着色後）入手後アイテムID22→35」
                            // 染色後 (ID29) をプレイヤーが拾えるように EnabletakeFlag を立てる。
                            // Gimmick.case 28 の FieldObjChange で名前が "29" になった後、
                            // 上部分岐 (EnabletakeFlag==1) 経由で Inventory に加わり、B絵画(22) に使用可能。
                            ItemDataBase.Entity.GetData(stageitemNumber + 1).EnabletakeFlag = 1;
                        }
                        break;
                    case 31://青ランプ(消灯)
                        StageItemGimmickOn();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        break;
                    case 32://青ランプ(点灯)
                        StageItemGimmickOn();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 0;
                        break;
                    case 33://水抜きスイッチ
                        StageItemGimmickOn();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        break;
                    case 37://黄ランプ(消灯)
                        StageItemGimmickOn();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        break;
                    case 38://黄ランプ(点灯)
                        StageItemGimmickOn();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 0;
                        break;
                    case 40://オルゴール(シリンダーなし)
                        if (itemslot.itemid == 11)//シリンダー
                        {
                            //事象処理
                            StageItemGimmickOn();
                            Inventory.instance.Removed(itemslot.itemid);
                            itemslot.ItemUse();
                            ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                            ItemDataBase.Entity.GetData(stageitemNumber + 1).EnabletakeFlag = 1;
                        }
                        break;
                    case 42://赤ランプ(消灯)
                        StageItemGimmickOn();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        break;
                    case 43://赤ランプ(点灯)
                        StageItemGimmickOn();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 0;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// タップしたステージアイテム情報取得
    /// </summary>
    /// <remarks>
    /// 増殖バグ修正: 以前は Camera.main からの Physics2D.Raycast で対象を取り直していたが、
    /// コライダーが重なっていると「インベントリに追加されるアイテム(Raycastヒット先)」と
    /// 「非表示になるオブジェクト(this.gameObject)」がズレて増殖する原因だった。
    /// このメソッドは自分がタップされた時に呼ばれるので、対象は常に自分自身とする。
    /// また名前が数値でない場合は stageitemNumber を前回値のまま使わず処理を中断する。
    /// </remarks>
    void GetStageItemTapObjectInfo()
    {
        stageitemobj = null;

        if (!int.TryParse(gameObject.name, out var id))
        {
            Debug.LogWarning($"[DropItem] オブジェクト名 '{gameObject.name}' がアイテムIDとして解釈できないため処理をスキップします。");
            return;
        }

        stageitemobj = gameObject;
        stageitemNumber = id;

        //Debug.Log(stageitemobj);
    }

    /// <summary>
    /// ステージアイテムギミックスタートオン
    /// </summary>
    void StageItemGimmickOn()
    {
        if (stageitemobj == null) return;
        var gimmick = stageitemobj.GetComponent<Gimmick>();
        if (gimmick != null)
        {
            gimmick.GimmickFlag = true;
        }
    }

}
