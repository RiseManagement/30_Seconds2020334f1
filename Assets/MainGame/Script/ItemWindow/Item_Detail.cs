using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// アイテム詳細機能
/// </summary>
public class Item_Detail : MonoBehaviour
{
    public GameObject itemSlotcs;
    public bool isDetail = false;
    GameObject itemDetail;
    //アイテム詳細の表示非表示のどのアイテムの詳細を表示するかの判定
    // Start is called before the first frame update
    void Start()
    {
        itemDetail = GameObject.Find("ItemDetail_Canvas");
        if (itemDetail != null)
        {
            itemDetail.SetActive(false);
        }
        else
        {
            Debug.LogWarning("[Item_Detail] ItemDetail_Canvas が見つかりません。");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //DropItemのスクリプトのほうで呼び出すアイテム取得した時の詳細表示の関数
    public void ItemDropDetail(int number)
    {
        if (itemDetail == null) return;

        itemDetail.SetActive(true);
        int itemnumber = number;
        Sprite sprite;
        string detail;
        sprite = ItemDataBase.Entity.GetData(itemnumber).Image;
        detail = ItemDataBase.Entity.GetData(itemnumber).Explanation;
        Debug.Log(detail);

        Debug.Log(gameObject);
        Debug.Log("Item_Detail");
        //アイテム画像
        var image = itemDetail.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        image.sprite = sprite;
        image.preserveAspect = true;
        if (sprite != null)
        {
            const float max = 350f;
            float w = sprite.rect.width;
            float h = sprite.rect.height;
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
        itemDetail.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text = detail;
        //ItemDetailDisplaySwitch(itemDetail, isDetail=true);
    }

    //DropItemやItemDetailの選択によって詳細表示のONとOFFの切り替え関数
    public void ItemDetailDisplaySwitch(GameObject gameObject, bool displayswitch)
    {
        if (displayswitch == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //ItemDetail関連UIの非表示スイッチ用関数
    public void ItemDetailOff()
    {
        if (itemDetail == null) return;
        itemDetail.SetActive(false);
    }
}
