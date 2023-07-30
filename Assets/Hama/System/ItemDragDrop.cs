using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Vector2 prevPosition; //保存しておく初期position
    ItemSlot itemSlotcs;
    ItemWinowSlot ItemWinowSlot;

    private void Awake()
    {
        itemSlotcs = GameObject.Find("ItemSlot").GetComponent<ItemSlot>();
        ItemWinowSlot = gameObject.transform.parent.GetComponent<ItemWinowSlot>();
    }

    /// <summary>
    /// ドラッグ開始時に呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        prevPosition = transform.position;
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

        bool flg = false;

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("Slot"))
            {
                flg = true;
            }
        }

        if (flg)
        {
            itemSlotcs.SelectItem(ItemWinowSlot.item);
            transform.position = prevPosition;
            //Debug.Log("ドラックアンドドロップ");
        }
        else
        {
            transform.position = prevPosition;
        }
    }
}
