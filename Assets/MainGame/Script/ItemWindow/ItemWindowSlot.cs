using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemWindowSlot :MonoBehaviour, IPointerClickHandler
{
    public int itemid;
    public Sprite icon;
    public string explanation;
    bool select;
    public bool Select
    {
        set
        {
            select = value;
        }
        get
        {
            return select;
        }
    }


    private void Start()
    {

    }

    private void Update()
    {
        if (itemid == -1)
        {
            DataReset();
        }
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    public void AddItem(int itemID)
    {
        //Debug.Log(itemID);

        var itemdata = ItemDataBase.Entity.GetData(itemID);
        itemid = itemID;
        icon = itemdata.Image;
        explanation = itemdata.Explanation;

        transform.GetChild(0).gameObject.SetActive(true);

        var image = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        if (image != null)
        {
            image.sprite = icon;
            image.preserveAspect = true;
            if (icon != null)
            {
                const float max = 200f;
                float w = icon.rect.width;
                float h = icon.rect.height;
                if (w >= h)
                {
                    float height = max * (h / w);
                    image.rectTransform.sizeDelta = new Vector2(max, height);
                }
                else
                {
                    float width = max * (w / h);
                    image.rectTransform.sizeDelta = new Vector2(width, max);
                }
            }
        }

        var drag = this.gameObject.transform.GetChild(0).GetComponent<ItemDragDrop>();
        if (drag == null)
        {
            this.gameObject.transform.GetChild(0).gameObject.AddComponent<ItemDragDrop>();
        }
    }

    public void ClearSlot()
    {
        itemid = -1;
        icon = null;

        //Debug.Log("クリアスロット");

        var drag = this.gameObject.transform.GetChild(0).GetComponent<ItemDragDrop>();
        if (drag != null)
        {
            Destroy(drag);
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select = true;
    }


    /// <summary>
    /// アイテムデータ取得
    /// </summary>
    /// <param name="id"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public int GetItemData(int id, int owner)
    {
        //Debug.Log(id);
        var item = ItemDataBase.Entity.GetDataAll();

        for (int i = id; i < item.Length; i++)
        {
            if (ItemDataBase.Entity.GetData(i).OwnerFlag == owner)
            {
                id = i + 1;
                AddItem(i);
                break;
            }
        }
        return id;
    }

    void DataReset()
    {
        icon = null;
        explanation = null;
    }
}

