using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    
    public Image icon;
    TItem item;
    [SerializeField] string explanation;

    public void AddItem(TItem newItem)
    {
        var itemdata = ItemDataBase.Entity.GetData(newItem.id);
        icon.sprite = itemdata.Image;
        explanation = itemdata.Explanation;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
    }

}
