using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_MysteryClear : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
        // 製品ビルドではデバッグボタンを破棄 (Debug_Mode の一括破棄に加えた二重防御)
        Destroy(gameObject);
#endif
    }

    //ボタンに設定
    public void MysteryClear(int casenum)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        switch(casenum)
        {
            case 1:
                MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO1);
                break;
            case 2:
                MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO2);
                break;
            case 3:
                MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO3A);
                MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO3B);
                break;
            case 4:
                MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO4A);
                MysteryManager.MysteryClearSet(MysteryManager.MysteryType.NAZO4B);
                break;
        }
#endif
    }
}
