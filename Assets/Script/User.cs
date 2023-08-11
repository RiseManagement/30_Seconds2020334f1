using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{
    protected string username;
    public string User_name{ get { return username; } }

    //アイテムスロットにあるアイテム
    public ItemID itemID;
    public ItemData itemdata;

    //アイテムウィンドウ情報
    ItemWinowSlot itemWinowSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void GetItem()
    {
        var item = ItemDataBase.Entity.GetDataAll();
        //Item
        foreach(ItemData data in item)
        {
            
            if(data.OwnerFlag == 1)
            {
                //アイテムインベントリにアイテム追加処理
                //そのためにリソースフォルダのItemIDを取得する処理追加
                //itemWinowSlot.AddItem()
            }
            if (data.OwnerFlag == 2)
            {

            }

            if (transform.GetComponent<User_A>())
            {
                
            }
            if (transform.GetComponent<User_B>())
            {

            }
        }
    }
    //アイテムスロットからプレイヤーの保持してるアイテムを取得
    　//アイテムを保持してるか確認
    　//保持してる場合はアイテムスロット情報からアイテム情報を取得
    　//保持してない場合は渡すアイテム情報を取得

    //アイテムDBからプレイヤーの所持してるアイテム情報をアイテムウィンドウ情報に設定する
}
