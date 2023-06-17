using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot1 : MonoBehaviour
{
    
    public Image icon;
    TItem item;

    public void AddItem(TItem newItem)
    {

        item = newItem;
        icon.sprite = newItem.icon;
        
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
    }

}
