using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventry : MonoBehaviour
{

    public static Inventry instance;
    InventryUI InventryUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
        gameObject.SetActive(false);
        InventryUI = GetComponent<InventryUI>();
        InventryUI.UpdateUI();

    }

    public List<ItemID> items = new List<ItemID>();

    public void Add(ItemID item)
    {

        items.Add(item);
        InventryUI.UpdateUI();

    }

    public void Removed(ItemID item)
    {
        items.Remove(item);
    }

}
