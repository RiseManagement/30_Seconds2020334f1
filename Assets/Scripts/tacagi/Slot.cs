using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    ZZZItem receiveItem;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public bool IsEmpty()
    {
        if(receiveItem == null)
        {
            return true;
        }
        return false;
    }

    public void SetItem(ZZZItem item)//SlotにItemを表示させる
    {
        receiveItem = item;
        UpImage(item);

    }

    void UpImage(ZZZItem item)
    {
        image.sprite = item.sprite;

    }
}
