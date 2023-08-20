using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeopItem : MonoBehaviour, IPointerClickHandler
{
    public ItemID item;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"オブジェクト {name} がクリックされたよ！");
        Inventry.instance.Add(item.id);
        Destroy(gameObject);
    }

    public void Pickup()
    {
        //Debug.Log("OK");
    }



}
