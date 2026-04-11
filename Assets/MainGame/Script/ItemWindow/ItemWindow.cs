using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemWindow : MonoBehaviour, IPointerClickHandler
{
    public GameObject Inventory;

    private void Start()
    {
    }

    /// <summary>
    /// アイテムウィンドウ開く
    /// </summary>
    void ItemWindowOpen()
    {
        Inventory.SetActive(true);
    }

    /// <summary>
    /// アイテムウィンドウ閉じる
    /// </summary>
    void ItemWindowClose()
    {
        Inventory.SetActive(false);
    }

    /// <summary>
    /// アイテムウィンドウ開く閉じる切り替え
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inventory.activeSelf)
            ItemWindowClose();
        else
            ItemWindowOpen();
    }
}
