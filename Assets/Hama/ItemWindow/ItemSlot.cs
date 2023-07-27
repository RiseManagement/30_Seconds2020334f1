using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Sprite icon;
    ItemID item;
    public ItemID Item
    {
        get { return item; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectItem(ItemID selectitem)
    {
        if (!selectitem) return;

        var itemdata = ItemDataBase.Entity.GetData(selectitem.id);
        item = selectitem;  
        icon= itemdata.Image;

        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }
}
