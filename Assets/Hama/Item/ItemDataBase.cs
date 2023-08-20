using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// アイテムデータベース
/// </summary>
public class ItemDataBase : ScriptableObject
{
    [SerializeField] ItemData[] datas;

    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ItemDB";

    //MyScriptableObjectの実体
    private static ItemDataBase _entity;
    public static ItemDataBase Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<ItemDataBase>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }
            return _entity;
        }
    }

    /// <summary>
    /// CSVファイルインポート後アイテムデータ設定
    /// </summary>
#if UNITY_EDITOR
    public class ItemInportDataSet : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            string assetfile = "";
            foreach (string str in importedAssets)
            {
                //　IndexOfの引数は"/(読み込ませたいファイル名)"とする。
                if (str.IndexOf("/ItemDB.csv") != -1)
                {
                    Debug.Log("CSVファイルがあった!!!");
                    //　Asset直下から読み込む（Resourcesではないので注意）
                    TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                    //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
                    assetfile = str.Replace(".csv", ".asset");
                    Debug.Log(assetfile);
                    ItemDataBase cd = AssetDatabase.LoadAssetAtPath<ItemDataBase>(assetfile);
                    if (cd == null)
                    {
                        cd = new ItemDataBase();
                        AssetDatabase.CreateAsset(cd, assetfile);
                    }

                    cd.datas = CSVSerializer.Deserialize<ItemData>(textasset.text);
                    EditorUtility.SetDirty(cd);
                    AssetDatabase.SaveAssets();
                }
            }

            //ファイル存在確認
            var result = System.IO.File.Exists("Assets/Hama/Resources/ItemDB.asset");
            if (!result)
            {
                result = AssetDatabase.CopyAsset("Assets/Hama/Item/ItemDB.asset", "Assets/Hama/Resources/ItemDB.asset");
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
                AssetDatabase.SaveAssets();
            }
        }
    }
#endif

    /// <summary>
    /// 保存されているデータ
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>idのデータ</returns>
    public ItemData GetData(int id)
    {
        return datas[id];
    }

    public ItemData[] GetDataAll()
    {
        return datas;
    }
}

/// <summary>
/// アイテムデータ
/// </summary>
[System.Serializable]
public class ItemData
{
    [SerializeField] int id;
    public int Id
    {
        get{ return id; }
    }
    [SerializeField] string name;
    public string Name
    {
        get { return name; }
    }
    [SerializeField] string explanation;
    public string Explanation
    {
        get { return explanation; }
    }
    [SerializeField] Sprite image;
    public Sprite Image
    {
        get { return image; }
    }
    [SerializeField] int ownerFlag;
    public int OwnerFlag
    {
        get { return ownerFlag; }
        set { if(value <= 2 && value >= 0)
                {
                    ownerFlag = value;
                } 
        }
    }
    [SerializeField] int interactFlag;
    public int InteractFlag
    {
        get { return interactFlag; }
        set { if (value == 0 || value == 1)
                {
                    interactFlag = value;
                }
        }
    }
}

