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

    public List<TItem> items = new List<TItem>();

    public void Add(TItem item)
    {

        items.Add(item);
        InventryUI.UpdateUI();

    }

    public void Removed(TItem item)
    {
        items.Remove(item);
    }

}
