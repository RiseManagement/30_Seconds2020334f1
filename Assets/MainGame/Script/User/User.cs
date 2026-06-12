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
        var parent = GameObject.Find("InventoryParent");
        if (parent == null)
        {
            Debug.LogError("InventoryParent が見つかりません");
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
            var slot = child.GetComponent<ItemWindowSlot>();
            if (slot != null)
                itemWinowSlot.Add(slot); // 順番に子オブジェクトを取得 (Slot以外の子は除外)
        }

        // Findは非アクティブを検索できないため、2回目以降(既に非アクティブ)はnullになる → nullなら何もしない
        var inventoryObj = GameObject.Find("Inventory");
        if (inventoryObj != null)
            inventoryObj.SetActive(false);

        int id = 0;

        // Count - 1 だと最後のスロットが復元されないオフバイワンだったため Count に修正
        for (int i = 0; i < itemWinowSlot.Count; i++)
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
