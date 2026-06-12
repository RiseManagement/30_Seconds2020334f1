using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryManager : MonoBehaviour
{
    //フラグリスト（謎仕様数）
    public enum MysteryType
    {
        NAZO1,
        NAZO2,
        NAZO3A,
        NAZO3B,
        NAZO4A,
        NAZO4B,
        NAZOTYPEMAX
    }

    //謎クラスリスト（0:謎1 1:謎2 2:謎3A 3:謎3B 4:謎4A 5:謎4B）
    static bool[] mysterylist = new bool[(int)MysteryType.NAZOTYPEMAX];
    public static bool[] MysteryList
    {
        get { return mysterylist; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        // デバッグ用: F1で全謎クリア (製品ビルドには含めない)
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("デバック_クリアフラグON");
            MysteryClearSet(MysteryType.NAZO1);
            MysteryClearSet(MysteryType.NAZO2);
            MysteryClearSet(MysteryType.NAZO3A);
            MysteryClearSet(MysteryType.NAZO3B);
            MysteryClearSet(MysteryType.NAZO4A);
            MysteryClearSet(MysteryType.NAZO4B);
        }
#endif
    }

    /// <summary>
    /// 謎クリアフラグを立てる。
    /// (旧実装は switch で 6 分岐していたが、全分岐が同一処理だったので 1 行に集約)
    /// </summary>
    public static void MysteryClearSet(MysteryType type)
    {
        mysterylist[(int)type] = true;
    }

    /// <summary>
    /// 全謎クリアフラグを初期化 (周回プレイ用、GameStateReset から呼ばれる)
    /// </summary>
    public static void ResetAll()
    {
        for (int i = 0; i < mysterylist.Length; i++)
        {
            mysterylist[i] = false;
        }
    }

    /// <summary>
    /// 全謎クリアチェック
    /// </summary>
    public static bool MysteryAllClearCheck()
    {
        for (int i = 0; i < (int)MysteryType.NAZOTYPEMAX; i++)
        {
            if (!mysterylist[i]) return false;
        }
        return true;
    }
}
