using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Inventory :MonoBehaviour
{
    public static Inventory instance;
    InventoryUI InventoryUI;
    [SerializeField] Text explanationText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        //gameObject.SetActive(false);
        InventoryUI = GetComponent<InventoryUI>();
        if (InventoryUI == null)
        {
            Debug.LogError("InventoryUI が見つかりません");
            return;
        }
        RefreshForCurrentPlayer();
        InventoryUI.UpdateUI();

    }

    public List<int> itemsid = new List<int>();

    private void OnEnable()
    {
        if (InventoryUI == null)
        {
            InventoryUI = GetComponent<InventoryUI>();
        }
        RefreshForCurrentPlayer();
        if (InventoryUI != null)
        {
            InventoryUI.UpdateUI();
        }
    }

    /// <summary>
    /// インベントリーにアイテム追加
    /// </summary>
    /// <param name="itemid">アイテム</param>
    public void Add(int itemid)
    {
        if (!itemsid.Contains(itemid))
        {
            itemsid.Add(itemid);
            RefreshUI();
        }
    }

    /// <summary>
    /// UI 更新。Inventory が非アクティブのまま Start() が走らなかった場合でも
    /// (User.GetItem が Start 前に SetActive(false) することがある) null 参照で
    /// 取得処理が途中で止まらないようにする。
    /// </summary>
    private void RefreshUI()
    {
        if (InventoryUI == null)
        {
            InventoryUI = GetComponent<InventoryUI>();
        }
        if (InventoryUI != null)
        {
            InventoryUI.UpdateUI();
        }
    }

    /// <summary>
    /// インベントリーアイテム削除
    /// </summary>
    /// <param name="itemid">アイテムID</param>
    public void Removed(int itemid)
    {
        if (itemsid.Remove(itemid))
        {
            RefreshUI();
        }
    }

    public void SetExplanationText(string _explanationText)
    {
        explanationText.text = _explanationText;
    }

    private void RefreshForCurrentPlayer()
    {
        if (itemsid == null)
        {
            itemsid = new List<int>();
        }
        itemsid.Clear();

        // 前ターンの Player を拾わないよう User.CurrentPlayer 経由で判定する
        int ownerFlag = User.CurrentOwnerFlag;
        if (ownerFlag == 0)
        {
            return;
        }

        // 増殖防止: 手持ちスロット/パススロットにあるアイテムはインベントリ表示から除外する
        // (OwnerFlagは所有者のままなので、除外しないと再表示されて二重に見える)
        int handItemId = -1;
        var itemSlotObj = GameObject.Find("ItemSlot");
        if (itemSlotObj != null)
        {
            var slot = itemSlotObj.GetComponent<ItemSlot>();
            if (slot != null)
            {
                handItemId = slot.ItemId;
            }
        }

        var all = ItemDataBase.Entity.GetDataAll();
        for (int i = 0; i < all.Length; i++)
        {
            if (i == handItemId || i == PassSystem.passitemid)
            {
                continue;
            }
            if (ItemDataBase.Entity.GetData(i).OwnerFlag == ownerFlag)
            {
                itemsid.Add(i);
            }
        }
    }
}
