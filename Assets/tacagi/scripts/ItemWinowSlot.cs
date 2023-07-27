using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemWinowSlot : MonoBehaviour, IPointerClickHandler
{
    public ItemID item;
    public Sprite icon;
    string explanation;
    bool select;
    public bool Select
    {
        set
        {
            select = value;
        }
        get
        {
            return select;
        }
    }
    ItemSlot itemSlotcs;

    private void Start()
    {
        itemSlotcs = GameObject.Find("ItemSlot").GetComponent<ItemSlot>();
    }

    public void AddItem(ItemID newItem)
    {
        item = newItem;
        var itemdata = ItemDataBase.Entity.GetData(newItem.id);
        icon = itemdata.Image;
        explanation = itemdata.Explanation;

        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    public void ClearSlot()
    {
        item = null;
        icon = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select = true;
        itemSlotcs.SelectItem(item);
    }

}
