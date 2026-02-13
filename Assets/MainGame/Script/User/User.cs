using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{
    protected string username;
    public string User_name{ get { return username; } }

    //アイテムウィンドウ情報
    [SerializeField] List<ItemWindowSlot> itemWinowSlot;

    static public GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected void GetItem(GameObject playerobj)
    {
        var parent = GameObject.Find("InventryPalent");
        if (parent == null)
        {
            Debug.LogError("InventryPalent が見つかりません");
            return;
        }

        if (itemWinowSlot == null)
        {
            itemWinowSlot = new List<ItemWindowSlot>();
        }
        else
        {
            itemWinowSlot.Clear();
        }

        //Debug.Log(parent);

        foreach (Transform child in parent.transform)
        {
            itemWinowSlot.Add(child.GetComponent<ItemWindowSlot>()); // 順番に子オブジェクトを取得
        }

        GameObject.Find("Inventry").SetActive(false); 

        int id = 0;

        for (int i = 0; i < itemWinowSlot.Count - 1; i++)
        {
            if (playerobj.GetComponent<User_A>())
            {
               id = itemWinowSlot[i].GetItemData(id, 1);
            }
            if (playerobj.GetComponent<User_B>())
            {
               id = itemWinowSlot[i].GetItemData(id, 2);
            }
            //Debug.Log("id:"+id);
        }
    }

    protected void SceneSet(GameObject playerobj)
    {
        //Debug.Log(playerObj);
        if (SceneTransitions.OldSceneName == SceneTransitions.SceneName.INTERVAL.ToString().ToLower())
        {
            //Debug.Log("デストロイ解除");
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(playerObj,
                UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            Destroy(playerObj);
        }
        DontDestroyOnLoad(playerobj);
    }
}
