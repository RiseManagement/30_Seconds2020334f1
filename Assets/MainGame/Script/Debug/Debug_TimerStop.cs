using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デバッグ用：タイマーの停止／再開をトグルするボタン用スクリプト。
/// Timer.countstop を切り替え、ボタンのラベルを状態に合わせて更新する。
/// </summary>
public class Debug_TimerStop : MonoBehaviour
{
    [Tooltip("ボタンに表示するラベル。ボタンの子の Text を割り当てる。")]
    [SerializeField] private Text label;

    [Tooltip("タイマー停止中（countstop = true）に表示するラベル文字列。")]
    [SerializeField] private string stoppedLabel = "タイマー\n再開";

    [Tooltip("タイマー稼働中（countstop = false）に表示するラベル文字列。")]
    [SerializeField] private string runningLabel = "タイマー\n停止";

    private void Awake()
    {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
        // 製品ビルドではデバッグボタンを破棄 (Debug_Mode の一括破棄に加えた二重防御)
        Destroy(gameObject);
#endif
    }

    private void OnEnable()
    {
        // 表示が有効化されたタイミングでラベルを現在の状態に合わせる
        UpdateLabel();
    }

    /// <summary>
    /// OnClick からの呼び出し用。タイマーの停止／再開をトグルする。
    /// </summary>
    public void Toggle()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Timer.countstop = !Timer.countstop;
        UpdateLabel();
#endif
    }

    /// <summary>
    /// 明示的にタイマーを止めたい場合用。
    /// </summary>
    public void Stop()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Timer.countstop = true;
        UpdateLabel();
#endif
    }

    /// <summary>
    /// 明示的にタイマーを再開したい場合用。
    /// </summary>
    public void Resume()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Timer.countstop = false;
        UpdateLabel();
#endif
    }

    private void UpdateLabel()
    {
        if (label == null)
        {
            return;
        }
        label.text = Timer.countstop ? stoppedLabel : runningLabel;
    }
}
