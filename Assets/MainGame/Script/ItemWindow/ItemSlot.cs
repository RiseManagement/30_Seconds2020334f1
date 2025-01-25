using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Sprite icon;

    //使用中アイテムID
    public int itemid = -1;
    public int ItemId
    {
        get { return itemid; }
    }

    // Start is called before the first frame update
    void Start()
    {
        var id = PassSystem.passitemid;

        if(id != -1)
        {
            SelectItem(id);
        }

        //Debug.Log(id);
    }

    // Update is called once per frame
    void Update()
    {
        ItemUIActiveChange();
    }


    /// <summary>
    /// 所有アイテムリストからアイテムスロットのアイテム選択
    /// </summary>
    /// <param name="selectitemid"></param>
    public void SelectItem(int selectitemid)
    {
        var itemdata = ItemDataBase.Entity.GetData(selectitemid);
        itemid = selectitemid;
        icon = itemdata.Image;

        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    /// <summary>
    /// アイテム使用
    /// </summary>
    public void ItemUse()
    {
        if (itemid == -1) return;

        var itemdata = ItemDataBase.Entity.GetData(itemid);

        //使用済に更新
        itemdata.InteractFlag = 1;

        //所有者なしに更新
        itemdata.OwnerFlag = 0;
        itemid = -1;

        //アイテム画像なしに更新
        icon = null;
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;

        //Debug.Log("アイテム使用済");
    }

    /// <summary>
    /// アイテムUI活性非活性切り替え
    /// </summary>
    private void ItemUIActiveChange()
    {
        if(itemid != -1)
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        else
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
