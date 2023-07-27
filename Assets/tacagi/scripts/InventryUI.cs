﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventryUI : MonoBehaviour
{

    public Transform slotsParent;
   public ItemWinowSlot[] slots;

    private void Start()
    {
       
        slots = slotsParent.GetComponentsInChildren<ItemWinowSlot>();
    }

    public void UpdateUI()
    {

       for(int i=0; i<slots.Length; i++)
        {
            if (i < Inventry.instance.items.Count)
            {
                slots[i].AddItem(Inventry.instance.items[i]);
            }
            else
                slots[i].ClearSlot();
        }
    }
}
