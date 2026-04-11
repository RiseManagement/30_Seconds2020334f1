using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    //ランプオブジェクトリスト
    GameObject[] LampObjList = new GameObject[3];

    //点灯オブジェクトリスト
    GameObject[] LightingObjList = new GameObject[3];

    //ランプ回答順リスト
    List<GameObject> AnwserObjList = new List<GameObject>();

    Gimmick[] LampGimmickList = new Gimmick[3];
    bool[] lastGimmickFlags = new bool[3];

    //水抜きスイッチ
    public GameObject WaterSwitchObj;

    // Start is called before the first frame update
    void Start()
    {
        LampObjList[0] = GameObject.Find("31");
        LampObjList[1] = GameObject.Find("37");
        LampObjList[2] = GameObject.Find("42");

        for(int i = 0;i < LampGimmickList.Length;i++)
            LampGimmickList[i] = LampObjList[i].GetComponent<Gimmick>();
    }

    // Update is called once per frame
    void Update()
    {
        LampLightUpStore();
    }

    private void FixedUpdate()
    {
        // すでに謎3Bクリア済みなら以降の判定は不要
        if (MysteryManager.MysteryList[(int)MysteryManager.MysteryType.NAZO3B])
        {
            return;
        }

        if (AnwserObjList.Count >= 3)
        {
            if (AnwserObjList[0].name == "43" && AnwserObjList[1].name == "38" && AnwserObjList[2].name == "32")
            {
                Debug.Log("スイッチ出現");

                //スイッチ出現
                WaterSwitchObj.SetActive(true);

                // 仕様書 (ギミック発動詳細化一覧.xlsx 行9):
                // 「正しい点灯ならB部屋にアイテムID33出現。謎3Bクリア。」
                // ランプヒント紙(ID36) は EnableTake=1 で取得後に SetActive(false) されるため、
                // Gimmick.case 36 経由の発火経路は 36 の取得状態に依存してしまう。
                // ここでは ID36 のデータ反映と合わせて NAZO3B を直接クリアする。
                ItemDataBase.Entity.GetData(36).InteractFlag = 1;
                ItemDataBase.Entity.GetData(36).ClearCheck = 2;
                MysteryManager.MysteryClerSet(MysteryManager.MysteryType.NAZO3B);
            }
            else
            {
                AnwserObjList.Clear();
                //Debug.Log("ランプ点灯リセット");
            }
        }

    }

    /// <summary>
    /// ランプ点灯格納
    /// </summary>
    void LampLightUpStore()
    {
        for (int i = 0; i < LampGimmickList.Length; i++)
        {
            var gimmick = LampGimmickList[i];
            if (gimmick == null) continue;

            bool current = gimmick.gimmickFlag;
            if (current && !lastGimmickFlags[i])
            {
                AnwserObjList.Add(LampObjList[i]);
            }
            lastGimmickFlags[i] = current;
        }
    }

}
