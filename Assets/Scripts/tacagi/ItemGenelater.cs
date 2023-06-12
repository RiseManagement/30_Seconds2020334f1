using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenelater : MonoBehaviour
{
    [SerializeField] ItemList itemListEntity;

    public static ItemGenelater instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public ZZZItem Spawn(ZZZItem.Type type)
    {
        //リストの中からItmeのTypeと一致するものを生成して渡す
        foreach(ZZZItem item in itemListEntity.itemList)
        {
            if(item.type == type)
            {
                return new ZZZItem(item.type, item.sprite);
            }
            
        }
         return null;
    }

}
