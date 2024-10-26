using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Item_Diteal : MonoBehaviour
{
    public GameObject itemSlotcs;
    public bool isDiteal=false;
    public GameObject itemditeal;
    //アイテム詳細の表示非表示のどのアイテムの詳細を表示するかの判定
    // Start is called before the first frame update
    void Start()
    {
        itemditeal = GameObject.Find("ItemDiteal_Canvas");
        itemditeal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //DropItemのスクリプトのほうで呼び出すアイテム取得した時の詳細表示の関数
    public void ItemDropDiteal(int number)
    {
        itemditeal.SetActive(true);
        int itemnumber=number;
        Sprite sprite;
        string diteal;
        sprite=ItemDataBase.Entity.GetData(itemnumber).Image;
        diteal = ItemDataBase.Entity.GetData(itemnumber).Explanation;
        Debug.Log(diteal);
        GameObject gameObject=GameObject.Find("ItemDiteal_Canvas");
        
        Debug.Log(gameObject);
        Debug.Log("Item_Diteal");
        gameObject.transform.GetChild(1).GetComponent<Image>().sprite=sprite;
        gameObject.transform.GetChild(0).GetComponent<Text>().text=diteal;
        ItemDitealDisplaySwitch(gameObject,isDiteal=true);
        
    }

    //DropItemやItemDitealの選択によって詳細表示のONとOFFの切り替え関数
    public void ItemDitealDisplaySwitch(GameObject gameObject,bool displayswitch)
    {
        if(displayswitch==true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    //ItemDiteal関連UIの非表示スイッチ用関数
    public void ItemDitealOff()
    {
        itemditeal.SetActive(false);
    }

    
}
