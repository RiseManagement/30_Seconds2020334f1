using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassSystem : MonoBehaviour
{
    static public int passitemid = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ItemPass(GameObject playerobj)
    {
        //Debug.Log("パス");
        //Debug.Log(playerobj.name);
        //Debug.Log(passitemid);

        if (passitemid == -1) return;

        int passedItemId = passitemid;

        if (playerobj.GetComponent<User_A>())
        {
            //Debug.Log("Bに渡す");
            var item = ItemDataBase.Entity.GetData(passitemid);
            item.OwnerFlag = 2;
        }
        else if (playerobj.GetComponent<User_B>())
        {
            //Debug.Log("Aに渡す");
            var item = ItemDataBase.Entity.GetData(passitemid);
            item.OwnerFlag = 1;
        }
        else
        {
            return;
        }

        if (Inventory.instance != null)
        {
            Inventory.instance.Removed(passedItemId);
        }

        passitemid = -1;

        // 増殖防止: 渡したアイテムがスロットUIに残らないようクリアする
        var passSlotObj = GameObject.Find("PassSlot");
        if (passSlotObj != null)
        {
            var passSlot = passSlotObj.GetComponent<PassSlot>();
            if (passSlot != null && passSlot.ItemId == passedItemId)
            {
                passSlot.ClearSlot();
            }
        }
        var itemSlotObj = GameObject.Find("ItemSlot");
        if (itemSlotObj != null)
        {
            var itemSlot = itemSlotObj.GetComponent<ItemSlot>();
            if (itemSlot != null && itemSlot.ItemId == passedItemId)
            {
                itemSlot.ClearSlot();
            }
        }
    }
}
