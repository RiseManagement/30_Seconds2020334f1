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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("デバック_クリアフラグON");
            MysteryClerSet(MysteryType.NAZO1);
            MysteryClerSet(MysteryType.NAZO2);
            MysteryClerSet(MysteryType.NAZO3A);
            MysteryClerSet(MysteryType.NAZO3B);
            MysteryClerSet(MysteryType.NAZO4A);
            MysteryClerSet(MysteryType.NAZO4B);
        }
    }

    //謎クリアセット
    public static void MysteryClerSet(MysteryType misterynum)
    {
        switch (misterynum)
        {
            case MysteryType.NAZO1:
                mysterylist[(int)MysteryManager.MysteryType.NAZO1] = true;
                break;
            case MysteryType.NAZO2:
                mysterylist[(int)MysteryManager.MysteryType.NAZO2] = true;
                break;
            case MysteryType.NAZO3A:
                mysterylist[(int)MysteryManager.MysteryType.NAZO3A] = true;
                break;
            case MysteryType.NAZO3B:
                mysterylist[(int)MysteryManager.MysteryType.NAZO3B] = true;
                break;
            case MysteryType.NAZO4A:
                mysterylist[(int)MysteryManager.MysteryType.NAZO4A] = true;
                break;
            case MysteryType.NAZO4B:
                mysterylist[(int)MysteryManager.MysteryType.NAZO4B] = true;
                break;
        }
    }

    /// <summary>
    /// 全謎クリアチェック
    /// </summary>
    /// <returns></returns>
    public static bool MysteryAllClerCheck()
    {
        if(mysterylist[(int)MysteryManager.MysteryType.NAZO1] &&
            mysterylist[(int)MysteryManager.MysteryType.NAZO2] &&
            mysterylist[(int)MysteryManager.MysteryType.NAZO3A] &&
            mysterylist[(int)MysteryManager.MysteryType.NAZO3B] &&
            mysterylist[(int)MysteryManager.MysteryType.NAZO4A] &&
            mysterylist[(int)MysteryManager.MysteryType.NAZO4B])
        {
            return true;
        }

        return false;
    }
}
