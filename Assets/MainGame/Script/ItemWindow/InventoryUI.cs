using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
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
            if (i < Inventory.instance.itemsid.Count)
            {
                slots[i].AddItem(Inventory.instance.itemsid[i]);
            }
            else
                slots[i].ClearSlot();
        }
    }

    private void InitializeSlots()
    {
        if (slotsParent == null)
        {
            Debug.LogWarning("InventoryUI.slotsParent が設定されていません");
            return;
        }

        slots = slotsParent.GetComponentsInChildren<ItemWindowSlot>();
    }
}
