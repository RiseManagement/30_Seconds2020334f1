using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using System;

/// <summary>
/// アイテムデータベース
/// </summary>
public class ItemDataBase : ScriptableObject
{
    [SerializeField] ItemData[] datas;

    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ItemDB";

    //MyScriptableObjectの実体 (アセットそのものではなく、ランタイム専用のクローン)
    private static ItemDataBase _entity;
    public static ItemDataBase Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                var asset = Resources.Load<ItemDataBase>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (asset == null)
                {
                    Debug.LogError(PATH + " not found");
                    return null;
                }

                // アセットを直接使わずクローンを返す:
                // ・ランタイム中のフラグ書き換えがエディタのアセットに永続化される汚染を防ぐ
                // ・周回プレイ時は ResetRuntimeData() でクローンを破棄すれば初期状態に戻る
                _entity = Instantiate(asset);
            }
            return _entity;
        }
    }

    /// <summary>
    /// ランタイムデータを破棄し、次回アクセス時にアセットから再ロードさせる。
    /// タイトルシーン読込時に自動で呼ばれる (周回プレイ対応)。
    /// </summary>
    public static void ResetRuntimeData()
    {
        if (_entity != null)
        {
            Destroy(_entity);
            _entity = null;
        }
    }

    static bool _autoResetRegistered;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void RegisterAutoReset()
    {
        // Enter Play Mode Options (ドメインリロード無効) でも安全なように
        // 起動時に必ずクリーンな状態から始め、二重購読も防ぐ
        ResetRuntimeData();
        if (_autoResetRegistered) return;
        _autoResetRegistered = true;

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) =>
        {
            // タイトルに戻るたびに進行フラグを初期化する
            if (scene.name.Equals("title", StringComparison.OrdinalIgnoreCase))
            {
                ResetRuntimeData();
            }
        };
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
                        // ScriptableObject は new ではなく CreateInstance で生成する
                        cd = ScriptableObject.CreateInstance<ItemDataBase>();
                        AssetDatabase.CreateAsset(cd, assetfile);
                    }

                    cd.datas = CSVSerializer.Deserialize<ItemData>(textasset.text);
                    EditorUtility.SetDirty(cd);

                    // ゲームが実際にロードする Resources 側のアセットにも常に反映する
                    // (従来は「存在しない時にコピー」のみで、CSV再インポートが反映されなかった)
                    const string resPath = "Assets/MainGame/Resources/ItemDB.asset";
                    var resCd = AssetDatabase.LoadAssetAtPath<ItemDataBase>(resPath);
                    if (resCd == null)
                    {
                        if (AssetDatabase.CopyAsset(assetfile, resPath))
                            Debug.Log("Resources/ItemDB.asset を新規作成");
                        else
                            Debug.LogError("Resources/ItemDB.asset のコピー失敗");
                    }
                    else
                    {
                        resCd.datas = CSVSerializer.Deserialize<ItemData>(textasset.text);
                        EditorUtility.SetDirty(resCd);
                        Debug.Log("Resources/ItemDB.asset を更新");
                    }
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
#endif

    /// <summary>
    /// 保存されているデータ
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>idのデータ</returns>
    // 不正ID参照時に返すダミー (全フラグ0)。クラッシュさせずログで気付けるようにする
    static ItemData _invalidData;

    public ItemData GetData(int id)
    {
        if (datas == null || id < 0 || id >= datas.Length)
        {
            Debug.LogError("[ItemDataBase] 不正なアイテムID参照: " + id +
                " (有効範囲: 0-" + ((datas != null ? datas.Length : 0) - 1) + ")");
            if (_invalidData == null) _invalidData = new ItemData();
            return _invalidData;
        }
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
        get { return id; }
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

    [SerializeField] int ownerflag;
    public int OwnerFlag
    {
        get { return ownerflag; }
        set
        {
            if (value <= 2 && value >= 0)
            {
                ownerflag = value;
            }
        }
    }
    [SerializeField] int interactflag;
    public int InteractFlag
    {
        get { return interactflag; }
        set
        {
            if (value == 0 || value == 1)
            {
                interactflag = value;
            }
        }
    }

    [SerializeField] int enabletake;
    public int EnabletakeFlag
    {
        get { return enabletake; }
        set
        {
            if (value == 0 || value == 1)
            {
                enabletake = value;
            }
        }
    }

    [SerializeField] int focuspower;
    public int FocusPower
    {
        get { return focuspower; }
    }

    [SerializeField] int clearcheck;
    public int ClearCheck
    {
        get { return clearcheck; }
        set
        {
            if (value == 2)
            {
                clearcheck = value;
            }
        }
    }
}

