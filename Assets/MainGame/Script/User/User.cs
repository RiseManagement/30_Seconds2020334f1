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

    /// <summary>
    /// 現在ターンのプレイヤーオブジェクト。
    /// DontDestroyOnLoad で残った前ターンの Player を GameObject.Find("Player") が拾ってしまう
    /// 問題を避けるため、各所ではこのプロパティ経由で取得する。
    /// </summary>
    public static GameObject CurrentPlayer
    {
        get
        {
            if (playerObj != null) return playerObj;
            return GameObject.Find("Player");
        }
    }

    /// <summary>
    /// 現在ターンのプレイヤーの所有者フラグ (A=1, B=2, 不明=0)
    /// </summary>
    public static int CurrentOwnerFlag
    {
        get
        {
            var p = CurrentPlayer;
            if (p == null) return 0;
            if (p.GetComponent<User_A>()) return 1;
            if (p.GetComponent<User_B>()) return 2;
            return 0;
        }
    }

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
        // 前ターンの Player (DontDestroyOnLoad で生存中) を必ず破棄する。
        // 旧実装は「前シーン名 == interval」のときだけ破棄していたが、SceneTransitions が
        // インターバル中はシーン名を更新しないため条件が成立せず、User_A / User_B が
        // ターンごとに DontDestroyOnLoad に溜まり続けていた。
        // その結果 GameObject.Find("Player") がどの Player を返すか不定になり、
        // OwnerFlag の付与先・パス先・インベントリ表示が狂ってアイテムが増殖/消失していた。
        if (playerObj != null && playerObj != playerobj)
        {
            // Destroy はフレーム末まで遅延するため、同フレーム内の Find("Player") に拾われないよう
            // 先に名前を変えて無効化しておく
            playerObj.name = "Player_Old";
            playerObj.SetActive(false);
            Destroy(playerObj);
        }
        playerObj = playerobj;
        DontDestroyOnLoad(playerobj);
    }
}
