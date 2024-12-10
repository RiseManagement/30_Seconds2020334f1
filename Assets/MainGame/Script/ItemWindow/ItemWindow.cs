using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemWindow : MonoBehaviour, IPointerClickHandler
{
    public GameObject Inventry;

    private void Start()
    {
    }

    /// <summary>
    /// アイテムウィンドウ開く
    /// </summary>
    void ItemWindowOpen()
    {
        Inventry.SetActive(true);
    }

    /// <summary>
    /// アイテムウィンドウ閉じる
    /// </summary>
    void ItemWindowClose()
    {
        Inventry.SetActive(false);
    }

    /// <summary>
    /// アイテムウィンドウ開く閉じる切り替え
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inventry.activeSelf)
            ItemWindowClose();
        else
            ItemWindowOpen();
    }
}
