using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_MysteryClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタンに設定
    public void MysteryCler(int casenum)
    {
        switch(casenum)
        {
            case 1:
                MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO1);
                break;
            case 2:
                MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO2);
                break;
            case 3:
                MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO3A);
                MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO3B);
                break;
            case 4:
                MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO4A);
                MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO4B);
                break;
        }
    }
}
