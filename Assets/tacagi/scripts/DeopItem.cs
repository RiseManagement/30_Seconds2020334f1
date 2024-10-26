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


    private void Start()
    {
        itemslot = GameObject.Find("ItemSlot").GetComponent<ItemSlot>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"オブジェクト {name} がクリックされたよ！");
        GetStageItemTapObjectInfo();
        stageitemNumber = int.Parse(stageitemobj.name);
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        //所持者がいない場合
        if (ItemDataBase.Entity.GetData(stageitemNumber).EnabletakeFlag == 1)
        {
            //アイテムの詳細画面の表示非表示をObject名を参照して行う
            //ここにitem_Ditealの関数を書く
            Item_Diteal item_Diteal=null;
            item_Diteal = GameObject.Find("MainGameManager").GetComponent<Item_Diteal>();
            item_Diteal.ItemDropDiteal(stageitemNumber);
            
            Inventry.instance.Add(stageitemNumber);
            Destroy(gameObject);
        }
        else
        {
            //camera.Focus(eventData.position);
            switch (stageitemNumber)
            {
                case 0://アイテム選択された側
                    if(itemslot.itemid == 12)//アイテム使用側
                    {
                        //事象処理
                        StageItemGimmickOn();
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    }
                    break;
                case 10:
                    Debug.Log("1");
                    stageitemobj.transform.GetChild(0).GetComponent<Piano>().isFocus = true;
                    camera.ItemFocus(new Vector2(stageitemobj.transform.position.x, stageitemobj.transform.position.y), 3);
                    break;
                case 11://アイテム選択された側
                    if (itemslot.itemid == 40)//アイテム使用側
                    {
                        //事象処理
                        StageItemGimmickOn();
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    }
                    break;
                case 14://アイテム選択された側
                    if (itemslot.itemid == 39)//アイテム使用側
                    {
                        //事象処理
                        StageItemGimmickOn();
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    }
                    break;
                case 15://アイテム選択された側
                    //事象処理
                    StageItemGimmickOn();
                    break;

            }

        }

    }

    void GetStageItemTapObjectInfo()
    {
        stageitemobj = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

        if (hit2d)
        {
            stageitemobj = hit2d.transform.gameObject;
        }

        Debug.Log(stageitemobj);
    }

    void StageItemGimmickOn()
    {
        stageitemobj.GetComponent<Gimmick>().GimmmickFlag = true;
    }

}
