using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Debug_ItemDB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemDBReset()
    {
        //ファイル存在確認
        var result = UnityEditor.AssetDatabase.CopyAsset("Assets/Hama/Item/ItemDB.asset", "Assets/Hama/Resources/ItemDB.asset");
        if (result)
        {
            //コピー成功
            Debug.Log("コピー成功");

        }
        else
        {
            //コピー失敗
            Debug.Log("コピー失敗");
        }
        UnityEditor.AssetDatabase.SaveAssets();
    }
}
