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
        if(!stageitemobj.name.Contains("_"))
        {
            stageitemNumber = int.Parse(stageitemobj.name);
        }
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        //所持者がいない場合
        if (ItemDataBase.Entity.GetData(stageitemNumber).EnabletakeFlag == 1)
        {
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
                case 3:
                    break;
                case 4:
                    if (!camera.Focusflg)
                    {
                        camera.ItemFocus(new Vector2(stageitemobj.transform.position.x, stageitemobj.transform.position.y), 3);
                        stageitemobj.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    break;
                case 10:
                    if (!camera.Focusflg)
                    {
                        camera.ItemFocus(new Vector2(stageitemobj.transform.position.x, stageitemobj.transform.position.y), 3);
                        stageitemobj.transform.GetChild(0).gameObject.SetActive(true);
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
                case 40://アイテム選択された側
                    if (itemslot.itemid == 11)//アイテム使用側
                    {
                        //事象処理
                        StageItemGimmickOn();
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();
                        ItemDataBase.Entity.GetData(stageitemNumber).InteractFlag = 1;
                    }
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
