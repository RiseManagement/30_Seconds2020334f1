using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField] ZZZItem.Type itemType;
    ZZZItem item;
    private void Start()
    {
        //どのオブジェクトがどういう情報を持っているかを格納しているリストからアイテムの情報を紐づける
        item = ItemGenelater.instance.Spawn(itemType);

    }

    public void OnClickObj()//オブジェクトをクリックするイベント
    {
        //オブジェクトをクリックしたアイテムをスロットに追加する
        ItemBox.instance.SetItem(item);

        //クリックしたオブジェクトを非表示
        gameObject.SetActive(false);

    }

}
