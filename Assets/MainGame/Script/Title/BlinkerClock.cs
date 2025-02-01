using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkerClock : MonoBehaviour
{
    // フェードアウトの設定（フェードアウト速度として使用）
    public float fadeDuration = 3.0f; // フェードアウトにかける時間（秒）
    public float maxScaleMultiplier = 2.0f; // フェードアウト時の最大サイズ倍率

    // 対象となるオブジェクト
    public GameObject Clock;
    public GameObject SecondHand;

    private Image ClockImage;
    private Image HandImage;

    void Start()
    {
        if (Clock != null)
        {
            ClockImage = Clock.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("Clock オブジェクトがアサインされていません");
        }

        if (SecondHand != null)
        {
            HandImage = SecondHand.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("SecondHand オブジェクトがアサインされていません");
        }
    }

    // フェードアウト処理：時計と秒針を同時にフェードアウト・拡大する
    public IEnumerator FadeOut()
    {
        // 初期状態の取得
        Color clockInitialColor = ClockImage.color;
        Vector3 clockOriginalScale = ClockImage.transform.localScale;
        Vector3 clockTargetScale = clockOriginalScale * maxScaleMultiplier;

        Color handInitialColor = HandImage.color;
        Vector3 handOriginalScale = HandImage.transform.localScale;
        Vector3 handTargetScale = handOriginalScale * maxScaleMultiplier;

        float elapsedTime = 0.0f;

        // fadeDuration 未満の間、両方の画像を更新
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // ClockImage の更新
            Color clockColor = clockInitialColor;
            clockColor.a = alpha;
            ClockImage.color = clockColor;
            ClockImage.transform.localScale = Vector3.Lerp(clockOriginalScale, clockTargetScale, elapsedTime / fadeDuration);

            // HandImage の更新
            Color handColor = handInitialColor;
            handColor.a = alpha;
            HandImage.color = handColor;
            HandImage.transform.localScale = Vector3.Lerp(handOriginalScale, handTargetScale, elapsedTime / fadeDuration);

            yield return null;
        }

        // 最終的に完全な状態にする（α = 0）
        {
            Color clockColor = clockInitialColor;
            clockColor.a = 0;
            ClockImage.color = clockColor;
            ClockImage.transform.localScale = clockTargetScale;

            Color handColor = handInitialColor;
            handColor.a = 0;
            HandImage.color = handColor;
            HandImage.transform.localScale = handTargetScale;
        }

        // 必要なら、オブジェクト全体を非アクティブにする
        gameObject.SetActive(false);
    }
}
