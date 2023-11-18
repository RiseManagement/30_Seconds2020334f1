﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeopItem : MonoBehaviour, IPointerClickHandler
{
    public ItemID item;
    public int itemslotid;
    ItemSlot itemslot;

    new CameraManager camera;
    public GameObject stageitemobj;


    private void Start()
    {
        itemslot = GameObject.Find("ItemSlot").GetComponent<ItemSlot>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"オブジェクト {name} がクリックされたよ！");

        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        //所持者がいない場合
        if (ItemDataBase.Entity.GetData(item.id).EnabletakeFlag == 1)
        {
            Inventry.instance.Add(item.id);
            Destroy(gameObject);
        }
        else
        {
            GetStageItemTapObjectInfo();
            //camera.Focus(eventData.position);
            switch (item.id)
            {
                case 0://アイテム選択された側
                    if(itemslot.itemid == 12)//アイテム使用側
                    {
                        Inventry.instance.Removed(itemslot.itemid);
                        itemslot.ItemUse();


                        //事象処理
                        StageItemGimmickOn();
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
