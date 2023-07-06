using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GetDB : MonoBehaviour
{
    //アイテムDB
    public static ScriptableObject itemDb;

    //Scriptable Object を取得
    [SerializeField] ItemDataBase itembase;

    //アイテム取得する時に初回場合ロードをはさむ（取得）
    
    //アイテム取得する時に初回場合ロードをはさむ（更新）

    //初期状態のスクリタブルオブジェクトの保持


    private void Awake()
    {
        //リソースフォルダからロード
        itembase = Resources.Load<ItemDataBase>("ItemDB");

        Debug.Log(itembase);

        //アンロード
        Resources.UnloadAsset(itembase);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(itembase.datas[0].Name);

        Debug.Log(itembase.datas[1].Name);
        itembase.datas[1].Name = "変更物";
        Debug.Log(itembase.datas[1].Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
