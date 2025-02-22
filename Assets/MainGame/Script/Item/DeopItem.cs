using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeopItem : MonoBehaviour, IPointerClickHandler
{
    ItemSlot itemslot;

    new CameraManager camera;
    public GameObject stageitemobj;
    public int stageitemNumber;

    //public int old_stageitemNumber;

    private void Start()
    {
        itemslot = GameObject.Find("ItemSlot").GetComponent<ItemSlot>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraManager>();
    }

    /// <summary>
    /// アイテムをタップ時の処理
    /// </summary>
    /// <param name="eventData">タップ情報</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"オブジェクト {name} がクリックされたよ！");
        GetStageItemTapObjectInfo();

        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        //所持可能なアイテムをタップした場合
        if (ItemDataBase.Entity.GetData(stageitemNumber).EnabletakeFlag == 1)
        {
            //所持する前の操作
            switch (stageitemNumber)
            {
                case 21://A宝箱(A鍵)
                    if (!MysteryManager.MysteryAllClerCheck()) return;
                    break;
                case 30://B宝箱（B鍵）
                    if (!MysteryManager.MysteryAllClerCheck()) return;
                    break;
                case 41://オルゴール(シリンダーあり)
                    stageitemNumber = 11;
                    break;
                default:
                    break;
            }

            //イベントり追加
            //アイテムの詳細画面の表示非表示をObject名を参照して行う
            //ここにitem_Ditealの関数を書く
            Item_Diteal item_Diteal=null;
            item_Diteal = GameObject.Find("ItemDitealController").GetComponent<Item_Diteal>();
            item_Diteal.ItemDropDiteal(stageitemNumber);
            
            //イベントリ追加
            Inventry.instance.Add(stageitemNumber);
            GameObject playerObj = GameObject.Find("Player").gameObject;

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

            //ステージ上のアイテム変化
            switch (stageitemNumber)
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
            switch (stageitemNumber)
            {
                case 0://アイテム選択された側
                    if(itemslot.itemid == 12)//アイテム使用側
                    {
                        StageItemGimmickOn();
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();

                        //アイテム選択側は使用済み更新
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    }
                    break;
                case 4://パズル
                    if (!camera.Focusflg)//フォーカスフラグ
                    {
                        camera.ItemFocus(new Vector2(stageitemobj.transform.position.x, stageitemobj.transform.position.y), 3);

                        //本体活性
                        stageitemobj.transform.GetChild(0).gameObject.SetActive(true);
                    }
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
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();

                        //アイテム選択側は使用済み更新
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    }
                    break;
                case 10://アップライトピアノ
                    if (!camera.Focusflg)//フォーカスフラグ
                    {
                        camera.ItemFocus(new Vector2(stageitemobj.transform.position.x, stageitemobj.transform.position.y), 3);

                        //本体活性
                        stageitemobj.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    break;
                case 14://台座
                    if (itemslot.itemid == 39)//リンゴ
                    {
                        //事象処理
                        StageItemGimmickOn();
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    }
                    break;
                case 15://台座(物乗っけてる)
                    StageItemGimmickOn();
                    break;
                case 17://水槽空
                    if (itemslot.itemid == 11)//シリンダー
                    {
                        //事象処理
                        StageItemGimmickOn();
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        ItemDataBase.Entity.GetData(itemslot.itemid).OwnerFlag = 0;
                    }
                    break;
                case 22://Ｂ絵画
                    if (itemslot.itemid == 29)//花瓶(染色後)
                    {
                        //事象処理
                        StageItemGimmickOn();
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
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
                        Inventry.instance.Removed(itemslot.itemid);
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
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                        ItemDataBase.Entity.GetData(stageitemNumber + 1).InteractFlag = 1;
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
                        Inventry.instance.Removed(itemslot.itemid);
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

    /// <summary>
    /// タップしたステージアイテム情報取得
    /// </summary>
    void GetStageItemTapObjectInfo()
    {
        stageitemobj = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

        if (hit2d)
        {
            stageitemobj = hit2d.transform.gameObject;
        }
        if (!stageitemobj.name.Contains("_"))
        {
            //ステージ名取得
            stageitemNumber = int.Parse(stageitemobj.name);
        }

        Debug.Log(stageitemobj);
    }

    /// <summary>
    /// ステージアイテムギミックスタートオン
    /// </summary>
    void StageItemGimmickOn()
    {
        stageitemobj.GetComponent<Gimmick>().GimmmickFlag = true;
    }

}
