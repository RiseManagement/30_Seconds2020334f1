using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerClickHandler
{
    // クリックされたときに呼び出されるメソッド
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"オブジェクト {name} がクリックされたよ！");
    }
}
