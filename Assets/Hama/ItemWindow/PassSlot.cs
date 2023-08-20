﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassSlot : MonoBehaviour
{
    Sprite icon;
    int itemid;
    public int ItemId
    {
        get { return itemid; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectItem(int selectitemid)
    {
        var itemdata = ItemDataBase.Entity.GetData(selectitemid);
        itemid = selectitemid;  
        icon= itemdata.Image;

        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }
}
