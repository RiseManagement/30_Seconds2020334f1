﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// アイテムデータベース
/// </summary>
public class Item : ScriptableObject
{
    public ItemData[] datas;
}

/// <summary>
/// アイテムデータ
/// </summary>
[System.Serializable]
public class ItemData
{
    public int ID;
    public string Name;
    public string Explanation;
    public Image Image;
    public int OwnerFlag;
    public int InteractFlag;
}

/// <summary>
/// CSVファイルインポート後アイテムデータ設定
/// </summary>
#if UNITY_EDITOR
public class ItemInportDataSet : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            //　IndexOfの引数は"/(読み込ませたいファイル名)"とする。
            if (str.IndexOf("/ItemDB.csv") != -1)
            {
                Debug.Log("CSVファイルがあった!!!");
                //　Asset直下から読み込む（Resourcesではないので注意）
                TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
                string assetfile = str.Replace(".csv", ".asset");
                Item cd = AssetDatabase.LoadAssetAtPath<Item>(assetfile);
                if (cd == null)
                {
                    cd = new Item();
                    AssetDatabase.CreateAsset(cd, assetfile);
                }

                cd.datas = CSVSerializer.Deserialize<ItemData>(textasset.text);
                EditorUtility.SetDirty(cd);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
#endif