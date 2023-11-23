using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Sprite icon;
    public int itemid = -1;
    public int ItemId
    {
        get { return itemid; }
    }

    // Start is called before the first frame update
    void Start()
    {
        var id = PassSystem.passitemid;

        if(id != -1)
        {
            SelectItem(id);
        }

        //Debug.Log(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectItem(int selectitemid)
    {
        var itemdata = ItemDataBase.Entity.GetData(selectitemid);
        itemid = selectitemid;
        icon = itemdata.Image;

        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    public void ItemUse()
    {
        if (itemid == -1) return;

        var itemdata = ItemDataBase.Entity.GetData(itemid);
        itemdata.InteractFlag = 1;
        itemdata.OwnerFlag = 0;
        itemid = -1;
        icon = null;
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;

        Debug.Log("アイテム使用済");
    }
}
