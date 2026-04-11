using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragDrop :MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Vector2 prevPosition; //保存しておく初期position
    ItemSlot itemSlotcs;
    PassSlot passSlotcs;
    ItemWindowSlot itemwindowSlot;

    private void Awake()
    {
        var itemSlotObj = GameObject.Find("ItemSlot");
        if (itemSlotObj != null) itemSlotcs = itemSlotObj.GetComponent<ItemSlot>();

        var passSlotObj = GameObject.Find("PassSlot");
        if (passSlotObj != null) passSlotcs = passSlotObj.GetComponent<PassSlot>();

        if (transform.parent != null)
        {
            itemwindowSlot = transform.parent.GetComponent<ItemWindowSlot>();
        }

        if (itemSlotcs == null || passSlotcs == null || itemwindowSlot == null)
        {
            Debug.LogWarning("[ItemDragDrop] 必要な参照が取得できませんでした。" +
                " itemSlotcs=" + (itemSlotcs != null) +
                ", passSlotcs=" + (passSlotcs != null) +
                ", itemwindowSlot=" + (itemwindowSlot != null));
        }
    }

    /// <summary>
    /// ドラッグ開始時に呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        prevPosition = transform.position;
        if (Inventory.instance != null && itemwindowSlot != null)
        {
            Inventory.instance.SetExplanationText(itemwindowSlot.explanation);
        }
    }

    /// <summary>
    /// ドラッグ中に呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    /// <summary>
    /// ドラッグ終わりに呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();     
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (itemwindowSlot == null) break;

            if (hit.gameObject.CompareTag("ItemSlot"))
            {
                if (itemSlotcs != null)
                {
                    itemSlotcs.SelectItem(itemwindowSlot.itemid);
                }
            }
            else if (hit.gameObject.CompareTag("PassSlot"))
            {
                //Debug.Log("パススロットセット");
                if (passSlotcs != null)
                {
                    passSlotcs.SelectItem(itemwindowSlot.itemid);
                }
                PassSystem.passitemid = itemwindowSlot.itemid;
            }
        }
        transform.position = prevPosition;
    }

}
