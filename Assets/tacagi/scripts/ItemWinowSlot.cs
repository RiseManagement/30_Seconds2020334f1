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
    

    private void Start()
    {

    }

    public void AddItem(ItemID newItem)
    {
        item = newItem;
        var itemdata = ItemDataBase.Entity.GetData(newItem.id);
        icon = itemdata.Image;
        explanation = itemdata.Explanation;

        transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    public void ClearSlot()
    {
        item = null;
        icon = null;

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select = true;
    }

}

