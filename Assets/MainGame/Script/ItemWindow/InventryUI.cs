using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventryUI : MonoBehaviour
{

    public Transform slotsParent;
   public ItemWindowSlot[] slots;

    private void Start()
    {
        InitializeSlots();
    }

    /// <summary>
    /// アイテムUI更新
    /// </summary>
    public void UpdateUI()
    {
        if (slots == null || slots.Length == 0)
        {
            InitializeSlots();
        }

        if (slots == null || slots.Length == 0)
        {
            return;
        }

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

    private void InitializeSlots()
    {
        if (slotsParent == null)
        {
            Debug.LogWarning("InventryUI.slotsParent が設定されていません");
            return;
        }

        slots = slotsParent.GetComponentsInChildren<ItemWindowSlot>();
    }
}
