using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassSlot : MonoBehaviour
{
    Sprite icon;
    int itemid = -1;
    public int ItemId
    {
        get { return itemid; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ItemUIActiveChange();
    }

    /// <summary>
    /// アイテム選択
    /// </summary>
    /// <param name="selectitemid"></param>
    public void SelectItem(int selectitemid)
    {
        var itemdata = ItemDataBase.Entity.GetData(selectitemid);
        itemid = selectitemid;  
        icon= itemdata.Image;

        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    /// <summary>
    /// アイテムUI活性非活性切り替え
    /// </summary>
    private void ItemUIActiveChange()
    {
        if (itemid != -1)
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        else
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
