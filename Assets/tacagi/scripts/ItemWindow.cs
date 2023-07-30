using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemWindow : MonoBehaviour, IPointerClickHandler
{
    public GameObject game;

    void ItemWindowOpen()
    {
        game.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemWindowOpen();
    }
}
