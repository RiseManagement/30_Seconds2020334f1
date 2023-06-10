using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class ZZZItem 
{
    //アイテムの種類データ
    public enum Type
    {
        CubeObj,
        BallObj,

    }

    public Type type;
    public Sprite sprite;

    public ZZZItem(Type type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
    }



}
