using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デバッグ表示の切り替えコンポーネント。
/// debugObjects[0] は「デバッグ表示」ボタン本体（OFF状態のときに表示）。
/// debugObjects[1..] はデバッグ用UI（ON状態のときに表示。OFFボタンやデバッグ機能ボタン等）。
/// </summary>
public class Debug_Mode : MonoBehaviour
{
    public List<GameObject> debugObjects = new List<GameObject>();
    public bool debugmode = false;

    void Start()
    {
        // Inspectorで未設定の場合のみ、子オブジェクトから自動収集する。
        // （手動設定時の重複登録を防ぐ）
        if (debugObjects == null || debugObjects.Count == 0)
        {
            debugObjects = new List<GameObject>(transform.childCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                debugObjects.Add(transform.GetChild(i).gameObject);
            }
        }

        // 起動時の状態を反映
        ApplyDebugMode();
    }

    /// <summary>
    /// デバッグ表示ON
    /// </summary>
    public void DebugModeOn()
    {
        debugmode = true;
        ApplyDebugMode();
    }

    /// <summary>
    /// デバッグ表示OFF
    /// </summary>
    public void DebugModeOff()
    {
        debugmode = false;
        ApplyDebugMode();
    }

    /// <summary>
    /// debugmode に応じて子オブジェクトの表示を切り替える。
    /// 状態変化時のみ呼ばれるため、Update() での毎フレーム処理は不要。
    /// </summary>
    private void ApplyDebugMode()
    {
        if (debugObjects == null || debugObjects.Count == 0)
        {
            return;
        }

        // [0] は「デバッグ表示」ボタン本体（OFFの時に表示）
        if (debugObjects[0] != null)
        {
            debugObjects[0].SetActive(!debugmode);
        }

        // [1..] はデバッグ用UI（ONの時に表示）
        for (int i = 1; i < debugObjects.Count; i++)
        {
            if (debugObjects[i] != null)
            {
                debugObjects[i].SetActive(debugmode);
            }
        }
    }
}
