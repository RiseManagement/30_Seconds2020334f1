using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeopItem : MonoBehaviour, IPointerClickHandler
{
    public ItemID item;
    public int itemslotid;
    ItemSlot itemslot;


    private void Start()
    {
        itemslot = GameObject.Find("ItemSlot").GetComponent<ItemSlot>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"オブジェクト {name} がクリックされたよ！");

        //所持者がいない場合
        if(ItemDataBase.Entity.GetData(item.id).OwnerFlag == 0)
        {
            Inventry.instance.Add(item.id);
            Destroy(gameObject);
        }
        else
        {
            switch (item.id)
            {
                case 6:
                    itemslot.ItemUse();
                    //事象処理
                    break;
            }

            Inventry.instance.Removed(itemslot.itemid);
        }

    }

    public void Pickup()
    {
        //Debug.Log("OK");
    }



}
