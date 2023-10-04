using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventryUI : MonoBehaviour
{

    public Transform slotsParent;
   public ItemWindowSlot[] slots;

    private void Start()
    {
       
        slots = slotsParent.GetComponentsInChildren<ItemWindowSlot>();
    }

    public void UpdateUI()
    {
       for(int i=0; i<slots.Length; i++)
        {
            if (i < Inventry.instance.itemsid.Count)
            {
                slots[i].AddItem(Inventry.instance.itemsid[i]);
            }
            else
                slots[i].ClearSlot();
        }
    }
}
