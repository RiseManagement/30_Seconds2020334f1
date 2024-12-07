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

    void ItemWindowOpen()
    {
        Inventry.SetActive(true);
    }

    void ItemWindowClose()
    {
        Inventry.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inventry.activeSelf)
            ItemWindowClose();
        else
            ItemWindowOpen();
    }
}
