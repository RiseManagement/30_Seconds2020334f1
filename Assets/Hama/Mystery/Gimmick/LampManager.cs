using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    //ランプオブジェクトリスト
    public GameObject[] LampObjList = new GameObject[3];

    //点灯オブジェクトリスト
    GameObject[] LightingObjList = new GameObject[3];

    //ランプ回答順リスト
    public List<GameObject> AnwserObjList = new List<GameObject>();

    public　Gimmick[] LampGimmickList = new Gimmick[3];

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

        //
        //WaterSwitchObj = GameObject.Find("33");
    }

    // Update is called once per frame
    void Update()
    {
        LampLightUpStore();
    }

    private void FixedUpdate()
    {
        if (AnwserObjList.Count >= 3)
        {
            if (AnwserObjList[0].name == "43" && AnwserObjList[1].name == "38" && AnwserObjList[2].name == "32")
            {
                Debug.Log("スイッチ出現");
                
                //ランプは使用済み
                ItemDataBase.Entity.GetData(int.Parse(AnwserObjList[0].name)).InteractFlag = 1;
                ItemDataBase.Entity.GetData(int.Parse(AnwserObjList[1].name)).InteractFlag = 1;
                ItemDataBase.Entity.GetData(int.Parse(AnwserObjList[2].name)).InteractFlag = 1;

                //スイッチ出現（統合後触る）
                //WaterSwitchObj.SetActive(true);
            }
            else
            {
                AnwserObjList.Clear();
                Debug.Log("ランプ点灯リセット");
            }
        }

    }

    /// <summary>
    /// ランプ点灯格納
    /// </summary>
    void LampLightUpStore()
    {
        if (LampGimmickList[0].gimmmickFlag)
        {
            AnwserObjList.Add(LampObjList[0]);
        }
        if (LampGimmickList[1].gimmmickFlag)
        {
            AnwserObjList.Add(LampObjList[1]);
        }
        if (LampGimmickList[2].gimmmickFlag)
        {
            AnwserObjList.Add(LampObjList[2]);
        }
    }

}
