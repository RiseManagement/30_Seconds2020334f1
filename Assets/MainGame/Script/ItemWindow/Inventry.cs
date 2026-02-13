using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Inventry :MonoBehaviour
{
    public static Inventry instance;
    InventryUI InventryUI;
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
        InventryUI = GetComponent<InventryUI>();
        if (InventryUI == null)
        {
            Debug.LogError("InventryUI が見つかりません");
            return;
        }
        RefreshForCurrentPlayer();
        InventryUI.UpdateUI();

    }

    public List<int> itemsid = new List<int>();

    private void OnEnable()
    {
        if (InventryUI == null)
        {
            InventryUI = GetComponent<InventryUI>();
        }
        RefreshForCurrentPlayer();
        if (InventryUI != null)
        {
            InventryUI.UpdateUI();
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
            InventryUI.UpdateUI();
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
            InventryUI.UpdateUI();
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

        var playerObj = GameObject.Find("Player");
        if (playerObj == null)
        {
            return;
        }

        int ownerFlag = 0;
        if (playerObj.GetComponent<User_A>())
        {
            ownerFlag = 1;
        }
        else if (playerObj.GetComponent<User_B>())
        {
            ownerFlag = 2;
        }
        else
        {
            return;
        }

        var all = ItemDataBase.Entity.GetDataAll();
        for (int i = 0; i < all.Length; i++)
        {
            if (ItemDataBase.Entity.GetData(i).OwnerFlag == ownerFlag)
            {
                itemsid.Add(i);
            }
        }
    }
}
