using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemWinowSlot : MonoBehaviour, IPointerClickHandler
{
    public int  itemid;
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

    public void AddItem(int itemID)
    {
        //Debug.Log(itemID);

        var itemdata = ItemDataBase.Entity.GetData(itemID);
        itemid = itemID;
        icon = itemdata.Image;
        explanation = itemdata.Explanation;

        transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    public void ClearSlot()
    {
        itemid = -1;
        icon = null;

        //transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select = true;
    }

    public int GetItemData(int id, int owner)
    {
        //Debug.Log(id);
        var item = ItemDataBase.Entity.GetDataAll();

        for(int i = id; i < item.Length; i++)
        {
            if (ItemDataBase.Entity.GetData(i).OwnerFlag == owner)
            {
                id = i+1;
                AddItem(i);
                break;
            }
        }
        return id;
    }
}

